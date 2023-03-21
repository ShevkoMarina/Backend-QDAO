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

        Task<T> QuerySingleIrDefaultAsync<T>(string command, CancellationToken ct, object parameters = null);
        Task<T> QueryAsync<T>(string command, CancellationToken ct, object parameters = null);
    }
}
