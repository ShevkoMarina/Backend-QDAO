using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence
{
    public interface IDapperExecutor
    {
        Task<DbConnection> OpenConnectionAsync(CancellationToken ct);
        Task<T> QuerySingleOrDefaultAsync<T>(string command, CancellationToken ct, object parameters = null);
        Task<IReadOnlyCollection<T>> QueryAsync<T>(string command, CancellationToken ct, object parameters = null);
        Task ExecuteAsync(string command, DbConnection connection, CancellationToken ct, object parameters = null);
    }
}
