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
        private readonly IDapperExecutor _database;
        private readonly ProposalRepository _proposalRepository;

        public ProposalStateTransitionPipeline(ContractsManager manager,
            IDapperExecutor database,
            ProposalRepository proposalRepository)
        {
            web3 = manager.Web3;
            _database = database;
            _proposalRepository = proposalRepository;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var currentBlock = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

                var proposalVotingPeriodInfo = await _database.QuerySingleOrDefaultAsync<ProposalVotingPeriodInfo>(
                    GetOldestCreatedProposal,
                    stoppingToken);

                if (proposalVotingPeriodInfo == default)
                {
                    await Task.Delay(1000);
                    break;
                }

                if (proposalVotingPeriodInfo.StartBlock >= (long)currentBlock.Value)
                {
                    var connection = await _database.OpenConnectionAsync(stoppingToken);
                    await _proposalRepository.InsertState(
                        Domain.ProposalState.Active,
                        (uint)proposalVotingPeriodInfo.Id,
                        connection,
                        CancellationToken.None);
                }
            }
        }

        private const string GetOldestCreatedProposal = @"--GetOldestCreatedProposal
                                                            select
                                                            id as Id,
                                                            start_block as StartBlock,
                                                            end_block as EndBlock
                                                            from proposal p
                                                            join lateral (
                                                                select state, created_at
                                                                from proposal_state_log psl
                                                                where psl.proposal_id = p.id
                                                                order by psl.created_at DESC NULLS LAST
                                                                limit 1
                                                                ) AS proposal_state on true
                                                            where proposal_state.state = 0
                                                            order by p.start_block
                                                            limit 1;";

        public class ProposalVotingPeriodInfo
        {
            public long Id { get; set; }
            public int StartBlock { get; set; }
            public int EndBlock { get; set; }
        }
          
    }
}
