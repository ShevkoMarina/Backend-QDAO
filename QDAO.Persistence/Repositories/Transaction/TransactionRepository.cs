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
    }
}
