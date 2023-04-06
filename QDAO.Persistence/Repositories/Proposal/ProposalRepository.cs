using System;
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
                           start_block = (long)proposal.StartBlock,
                           end_block = (long)proposal.EndBlock,
                           proposer_id = (long)proposal.Proposer,
                       }
                   );
            }
            catch (Exception ex)
            {

            }
        }
    }
}
