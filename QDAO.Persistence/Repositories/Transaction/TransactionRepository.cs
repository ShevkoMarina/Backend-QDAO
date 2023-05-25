using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.Transaction
{
    public class TransactionRepository
    {
        private readonly IDapperExecutor _database;

        public TransactionRepository(IDapperExecutor database)
        {
            _database = database;
        }

        public async Task SaveTransaction(string txHash, DbConnection connection, CancellationToken ct)
        {
            await _database.ExecuteAsync(
                TransactionSql.AddTransaction,
                connection,
                ct,
                new
                {
                    tx_hash = txHash
                }
            );
        }

        public async Task<TransactionQueue> GetNext(CancellationToken ct)
        {
            var queueId = await _database.QuerySingleOrDefaultAsync<TransactionQueue>(
                TransactionSql.GetNext,
                ct);

            return queueId;
        }

        public async Task SetProcessed(long queueId, DbConnection connection)
        {
            await _database.ExecuteAsync(
                TransactionSql.SetProccessed,
                connection,
                CancellationToken.None,
                new
                {
                    queue_id = queueId
                });
        }


        public async Task SetFailed(long queueId, DbConnection connection)
        {
            await _database.ExecuteAsync(
                TransactionSql.SetFailed,
                connection,
                CancellationToken.None,
                new
                {
                    queue_id = queueId
                });
        }
    }
}
