using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.ProposalQuorum
{
    public class ProposalQrisisQueueRepository
    {
        private readonly IDapperExecutor _database;

        public ProposalQrisisQueueRepository(IDapperExecutor database)
        {
            _database = database;
        }

        public async Task<ProposalWIthQueueId> GetNext(CancellationToken ct)
        {
            var result = await _database.QuerySingleOrDefaultAsync<ProposalWIthQueueId>(
                ProposalQuorumQueueSql.GetNext,
                ct);

            return result;
        }
    }
}
