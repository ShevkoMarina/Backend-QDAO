using QDAO.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;


namespace QDAO.Persistence.Repositories.Proposal
{
    public class ProposalRepository
    {
        private readonly IDapperExecutor _database;

        public ProposalRepository(IDapperExecutor database)
        {
            _database = database;
        }

        public async Task SaveProposal(Domain.Proposal proposal, DbConnection connection, CancellationToken ct)
        {
            try
            {
                await _database.ExecuteAsync(
                       ProposalSql.Add,
                       connection,
                       ct,
                       new
                       {
                           id = (long)proposal.Id,
                           start_block = (long)proposal.VotingInterval.StartBlock,
                           end_block = (long)proposal.VotingInterval.EndBlock,
                           proposer_id = (long)proposal.Proposer,
                       }
                   );
            }
            catch (Exception ex)
            {

            }
        }

        public async Task InsertState(ProposalState state, uint proposalId, DbConnection connection, CancellationToken ct)
        {
            await _database.ExecuteAsync(
                      ProposalSql.InsertState,
                      connection,
                      ct,
                      new
                      {
                          proposal_id = (long)proposalId,
                          state = (short)state
                      }
                  );
        }

        public async Task<IReadOnlyCollection<ProposalThin>> GetProposalsByProposerId(long proposerId, CancellationToken ct)
        {
            var proposals = await _database.QueryAsync<ProposalThin>(
                ProposalSql.GetByProposer,
                ct,
                new 
                {
                    proposer_id = proposerId
                }
             );

            return proposals;
        }
    }
}
