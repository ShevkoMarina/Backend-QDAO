using QDAO.Domain;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.User
{
    public class UserRepository
    {
        private readonly IDapperExecutor _database;

        public UserRepository(IDapperExecutor database)
        {
            _database = database;
        }

        public async Task<long> AddUser(
            string username,
            string password,
            string account,
            Roles role,
            DbConnection connection,
            CancellationToken ct)
        {

            var userId = await _database.QuerySingleOrDefaultAsync<long>(
               UserSql.Add,
               ct,
               connection,
               new
               {
                   login = username,
                   password = password,
                   account = account,
                   role = (short)role
               }
           );

            return userId;
        }

        public async Task<IReadOnlyCollection<Domain.User>> GetUsers(
            int[] userIds,
            CancellationToken ct)
        {
            var users = await _database.QueryAsync<Domain.User>(
                UserSql.GetByIds,
                ct,
                new
                {
                    userIds = userIds
                });

            return users;
        }

        public async Task<UserInfoDto> AuthorizeUser(string login, string password, CancellationToken ct)
        {
            var user = await _database.QuerySingleOrDefaultAsync<UserInfoDto>(
                UserSql.GetByLoginAndPassword,
                ct,
                new
                {
                    login = login,
                    password = password
                });


            return user;
        }
    }
}
