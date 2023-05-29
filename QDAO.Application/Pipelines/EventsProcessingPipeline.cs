using QDAO.Application.Pipelines.Events;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.Events;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Proposal;
using QDAO.Persistence.Repositories.Transaction;
using QDAO.Persistence.Repositories.User;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines
{
    public class EventsProcessingPipeline
    {
        private readonly ContractsManager _manager;
        private readonly ProposalRepository _proposalRepository;
        private readonly UserRepository _userRepository;
        private readonly TransactionRepository _transactionRepostiory;
        private readonly TransactionEventsDecoder _transactionEventsDecoder;
        private readonly IDapperExecutor _database;

        public EventsProcessingPipeline(ContractsManager manager,
            TransactionRepository transactionRepository,
            TransactionEventsDecoder transactionEventsDecoder,
            ProposalRepository proposalRepository,
            UserRepository userRepository,
            IDapperExecutor database)
        {
            _manager = manager;
            _transactionRepostiory = transactionRepository;
            _transactionEventsDecoder = transactionEventsDecoder;
            _proposalRepository = proposalRepository;
            _userRepository = userRepository;
            _database = database;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queuedTransaction = await _transactionRepostiory.GetNext(stoppingToken);
                // status and a 0x01 means success and 0x0

                // No transactions to handle
                if (queuedTransaction is null)
                {
                    await Task.Delay(1000, stoppingToken);
                    break;
                }

                // trying to get a receipt
                var receipt = await _manager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(queuedTransaction.Hash);

                // if transaction not mined
                if (receipt is null)
                {
                    break;
                }

                using var connection = await _database.OpenConnectionAsync(stoppingToken);

                // Transaction failed
                if (receipt.Status.Value == 0)
                {
                    await _transactionRepostiory.SetFailed(queuedTransaction.QueueId, connection);
                    break;
                }
          
                var decodedEvents = _transactionEventsDecoder.Decode(receipt);
                if (!decodedEvents.Any())
                {
                    await _transactionRepostiory.SetProcessed(queuedTransaction.QueueId, connection);
                    break;
                }
                var contractEvent = decodedEvents.First(); // Если нет событий то завершить

                if (contractEvent is ProposalCreatedEventDto proposalCreationEvent)
                {
                    await HandleProposalCreationEvent(proposalCreationEvent, stoppingToken);
                }
                if (contractEvent is ProposalQueuedEventDto proposalQueueEvent)
                {
                    await HandleProposalQueueEvent(proposalQueueEvent, stoppingToken);
                }
                if (contractEvent is VoteCastedEventDto voteCastedEvent)
                {
                    await HandleVoteCastedEvent(voteCastedEvent, stoppingToken);
                }
                if (contractEvent is PrincipalAddedEventDto principalAddedEvenet)
                {
                    await HandlePrincipalAddedEvent(principalAddedEvenet, stoppingToken);
                }
                if (contractEvent is ProposalApprovedEventDto proposalApprovedEvent)
                {
                    await HandleProposalApprovedEvent(proposalApprovedEvent, stoppingToken);
                }
                if (contractEvent is ProposalExecutedEventDto proposalExecutedEvent)
                {
                    await HandleProposalExecutedEvent(proposalExecutedEvent, stoppingToken);
                }
                                      
                await _transactionRepostiory.SetProcessed(queuedTransaction.QueueId, connection);                  
            }
        }
        
        // можно перенести в бэкграудн обработик а эти методы в хендлеры
        private async Task HandleProposalCreationEvent(ProposalCreatedEventDto proposalCreationEvent, CancellationToken stoppingToken)
        {
            using var connection = await _database.OpenConnectionAsync(stoppingToken);
            using var transaction = await connection.BeginTransactionAsync();
            try
            {
                var userId = await _userRepository.GetUserIdByAccount(proposalCreationEvent.Proposer, stoppingToken);
                var proposal = new Domain.Proposal() // todo сделать рекордом
                { 
                    Id = proposalCreationEvent.Id,
                    Name = proposalCreationEvent.Name,
                    Description = proposalCreationEvent.Description,
                    Proposer = userId,
                    VotingInterval = new VotingInterval(proposalCreationEvent.StartBlock, proposalCreationEvent.EndBlock),
                    ForVotes = 0,
                    AgainstVotes = 0
                };

                await _proposalRepository.SaveProposal(
                    new Domain.Proposal
                    {
                        Id = proposalCreationEvent.Id,
                        Proposer = userId,
                        Description = proposalCreationEvent.Description,
                        Name = "Преложение " + proposalCreationEvent.Id + ": " + proposalCreationEvent.Name
                    },
                    connection,
                    stoppingToken);

                await _proposalRepository.InsertState(
                    ProposalState.Pending,
                    proposalCreationEvent.Id,
                    connection,
                    stoppingToken);

                await _proposalRepository.InsertVotingInfo(proposal, connection, stoppingToken);

                await transaction.CommitAsync(); // проверить что нужен асинк
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
             }
        }

        private async Task HandleProposalQueueEvent(ProposalQueuedEventDto proposalQueueEvent, CancellationToken stoppingToken)
        {
            using var connection = await _database.OpenConnectionAsync(stoppingToken);
            await _proposalRepository.InsertState(
                ProposalState.Queued,
                proposalQueueEvent.Id,
                connection,
                stoppingToken);

            await _database.ExecuteAsync("update voting set eta = @eta where proposal_id = @proposal_id",
                connection,
                stoppingToken,
                new
                {
                    proposal_id = (long)proposalQueueEvent.Id,
                    eta = (long)proposalQueueEvent.Eta
                });
        }

        private async Task HandleVoteCastedEvent(VoteCastedEventDto voteCastedEvent, CancellationToken stoppingToken)
        {
            using var connection = await _database.OpenConnectionAsync(stoppingToken);

            if (voteCastedEvent.Support)
            {
                await _proposalRepository.AddVotesForProposal(
                voteCastedEvent.ProposalId,
                voteCastedEvent.Votes,
                connection,
                stoppingToken);
            }
            else
            {
                await _proposalRepository.AddVotesAgainstProposal(
                voteCastedEvent.ProposalId,
                voteCastedEvent.Votes,
                connection,
                stoppingToken);
            }
        }

        private async Task HandlePrincipalAddedEvent(PrincipalAddedEventDto principalAddedEvent, CancellationToken ct)
        {
            using var connection = await _database.OpenConnectionAsync(ct);

            await _database.ExecuteAsync("update users set role = 2 where account = @account",
                connection,
                ct,
                new
                {
                    account = principalAddedEvent.Principal
                });
        }

        private async Task HandleProposalApprovedEvent(ProposalApprovedEventDto proposalApprovedEvent, CancellationToken ct)
        {
            using var connection = await _database.OpenConnectionAsync(ct);
            await _proposalRepository.InsertState(
               ProposalState.Succeeded,
               (uint)proposalApprovedEvent.ProposalId,
               connection,
               ct);

        }

        private async Task HandleProposalExecutedEvent(ProposalExecutedEventDto proposalExecutedEvent, CancellationToken ct)
        {
            using var connection = await _database.OpenConnectionAsync(ct);
            await _proposalRepository.InsertState(
               ProposalState.Executed,
               (uint)proposalExecutedEvent.Id,
               connection,
               ct);
        }
    }
}
