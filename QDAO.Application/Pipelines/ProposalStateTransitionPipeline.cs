using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using QDAO.Application.Services;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Proposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines
{
    public class ProposalStateTransitionPipeline
    {
        private readonly Web3 web3;
        private readonly ContractsManager _manager;
        private readonly IDapperExecutor _database;
        private readonly ProposalRepository _proposalRepository;

        public ProposalStateTransitionPipeline(ContractsManager manager,
            IDapperExecutor database,
            ProposalRepository proposalRepository)
        {
            _manager = manager;
            web3 = manager.Web3;
            _database = database;
            _proposalRepository = proposalRepository;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentBlock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

                var oldestPendingProposal = await _database.QuerySingleOrDefaultAsync<ProposalVotingPeriodInfo>(
                    GetOldestCreatedProposal,
                    stoppingToken);

               

                if (oldestPendingProposal != default)
                {
                    if (oldestPendingProposal.StartBlock <= (long)currentBlock.Value)
                    {
                        var connection = await _database.OpenConnectionAsync(stoppingToken);
                        await _proposalRepository.InsertState(
                            Domain.ProposalState.Active,
                            (uint)oldestPendingProposal.Id,
                            connection,
                            CancellationToken.None);
                    }
                }

                var oldestActiveProposal = await _database.QuerySingleOrDefaultAsync<ProposalVotingPeriodInfo>(
                  GetOldestActiveProposal,
                  stoppingToken);

                if (oldestActiveProposal != default)
                {
                    if (oldestActiveProposal.EndBlock < (long)currentBlock.Value)
                    {
                        // Сделать запрос на получение статуса предложения
                        var handler = web3.Eth.GetContractQueryHandler<GetProposalState>();

                        var proposalState = await handler.QueryAsync<long>(_manager.GetGovernorDelegator(), new GetProposalState
                        {
                            ProposalId = oldestActiveProposal.Id
                        });

                        var state = (Domain.ProposalState)proposalState;

                        var connection = await _database.OpenConnectionAsync(stoppingToken);
                        await _proposalRepository.InsertState(
                          state,
                          (uint)oldestActiveProposal.Id,
                          connection,
                          CancellationToken.None);
                    }
                }

                await Task.Delay(1000);
            }
        }

        [Function("getProposalState", "uint256")]
        public class GetProposalState : FunctionMessage
        {
            [Parameter("uint256", "proposalId", 1)]
            public long ProposalId { get; set; }
        }

        private const string GetOldestCreatedProposal = @"--GetOldestCreatedProposal
                                                            select
                                                            id as Id,
                                                            v.start_block as StartBlock,
                                                            v.end_block as EndBlock
                                                            from proposal p
                                                            join voting v on p.id = v.proposal_id
                                                            join lateral (
                                                                select state, created_at
                                                                from proposal_state_log psl
                                                                where psl.proposal_id = p.id
                                                                order by psl.created_at DESC NULLS LAST
                                                                limit 1
                                                                ) AS proposal_state on true
                                                            where proposal_state.state = 0
                                                            order by v.start_block
                                                            limit 1";

        private const string GetOldestActiveProposal = @"--GetOldestCreatedProposal
                                                            select
                                                            id as Id,
                                                            v.start_block as StartBlock,
                                                            v.end_block as EndBlock
                                                            from proposal p
                                                            join voting v on p.id = v.proposal_id
                                                            join lateral (
                                                                select state, created_at
                                                                from proposal_state_log psl
                                                                where psl.proposal_id = p.id
                                                                order by psl.created_at DESC NULLS LAST
                                                                limit 1
                                                                ) AS proposal_state on true
                                                            where proposal_state.state = 1
                                                            order by v.start_block
                                                            limit 1";

        public class ProposalVotingPeriodInfo
        {
            public long Id { get; set; }
            public int StartBlock { get; set; }
            public int EndBlock { get; set; }
        }
          
    }
}
