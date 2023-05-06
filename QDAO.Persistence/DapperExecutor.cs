using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence
{
    public class DapperExecutor : IDapperExecutor
    {
        public async Task<DbConnection> OpenConnectionAsync(CancellationToken ct)
        {
            var connectionString = "User Id=postgres;Password=sukaBlyat123;Server=db.kidrgducdfhhwdjdvwoq.supabase.co;Port=5432;Database=postgres";
            var connection = new NpgsqlConnection(connectionString);

            await connection.OpenAsync(ct);
            return connection;
        }

        public async Task<IReadOnlyCollection<T>> QueryAsync<T>(string command, CancellationToken ct, object parameters = null)
        {
            await using var connection = await OpenConnectionAsync(ct);
            var result = await connection.QueryAsync<T>(BuildCommand(command, parameters, ct));

            return result.ToArray();
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string command, CancellationToken ct, object parameters = null)
        {
            await using var connection = await OpenConnectionAsync(ct);
            var result = await connection.QuerySingleOrDefaultAsync<T>(BuildCommand(command, parameters, ct));

            return result;
        }

        public Task ExecuteAsync(string command, DbConnection connection, CancellationToken ct, object parameters = null)
        {
            return connection.ExecuteAsync(BuildCommand(command, parameters, ct));
        }

        protected CommandDefinition BuildCommand(
            string commandText,
            object parameters,
            CancellationToken ct,
            DbTransaction transaction = null,
            int timeout = 500)
        {
            return new CommandDefinition(
                commandText,
                parameters,
                commandType: System.Data.CommandType.Text,
                commandTimeout: timeout,
                transaction: transaction,
                cancellationToken: ct
                );
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string command, CancellationToken ct, DbConnection connection, object parameters = null)
        {
            var result = await connection.QuerySingleAsync<T>(BuildCommand(command, parameters, ct));

            return result;
        }
    }
}
