using Nethereum.Contracts;
using QDAO.Application.Pipelines.Events;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.Events;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Proposal;
using QDAO.Persistence.Repositories.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines
{
    public class EventsProcessingPipeline
    {
        private readonly ContractsManager _manager;
        private readonly ProposalRepository _proposalRepository;
        private readonly TransactionRepository _transactionRepostiory;
        private readonly TransactionEventsDecoder _transactionEventsDecoder;
        private readonly IDapperExecutor _database;

        public EventsProcessingPipeline(ContractsManager manager,
            TransactionRepository transactionRepository,
            TransactionEventsDecoder transactionEventsDecoder,
            ProposalRepository proposalRepository,
            IDapperExecutor database)
        {
            _manager = manager;
            _transactionRepostiory = transactionRepository;
            _transactionEventsDecoder = transactionEventsDecoder;
            _proposalRepository = proposalRepository;
            _database = database;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var transactionQueued = await _transactionRepostiory.GetNext(stoppingToken);

                if (transactionQueued is null)
                {
                    await Task.Delay(1000, stoppingToken);
                    break;
                }

                var receipt = await _manager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionQueued.Hash);

                if (receipt is null)
                {
                    // 
                }

                var decodedEvents = _transactionEventsDecoder.Decode(receipt);

                var contractEvent = decodedEvents.First();

                if (contractEvent is ProposalCreatedEventDto)
                {
                    var proposalCreationEvent = (ProposalCreatedEventDto) contractEvent;

                    using var connection = await _database.OpenConnectionAsync(stoppingToken);
                    // добавить создание транзакции
                    await _proposalRepository.SaveProposal(
                        new Domain.Proposal
                        {
                            Id = proposalCreationEvent.Id,
                            VotingInterval = new VotingInterval(proposalCreationEvent.StartBlock,proposalCreationEvent.EndBlock),
                            Proposer = 1
                        }, 
                        connection,
                        stoppingToken);

                    await _proposalRepository.InsertState(
                        ProposalState.Active,
                        proposalCreationEvent.Id,
                        connection,
                        stoppingToken);
                }
                if (contractEvent is ProposalQueuedEventDto)
                {
                    var proposalQueueEvent = (ProposalQueuedEventDto)contractEvent;
                    using var connection = await _database.OpenConnectionAsync(stoppingToken);
                    await _proposalRepository.InsertState(
                        ProposalState.Active,
                        proposalQueueEvent.Id,
                        connection,
                        stoppingToken);
                }
            }
        }
    }
}
