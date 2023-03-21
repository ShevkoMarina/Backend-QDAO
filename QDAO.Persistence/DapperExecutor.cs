using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence
{
    public class DapperExecutor : IDapperExecutor
    {
        public Task<DbConnection> OpenConnectionAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<T> QueryAsync<T>(string command, CancellationToken ct, object parameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<T> QuerySingleIrDefaultAsync<T>(string command, CancellationToken ct, object parameters = null)
        {
            throw new NotImplementedException();
        }
    }
}
