using QDAO.Domain;
using System;
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

        public async Task<Domain.User> GetUserById(
            int userId,
            CancellationToken ct)
        {
            var user = await _database.QuerySingleOrDefaultAsync<Domain.User>(
                UserSql.GetByIds,
                ct,
                new
                {
                    userId = userId
                });

            return user;
        }

        public async Task<string> GetUserAccountById(
          int userId,
          CancellationToken ct)
        {
            try
            {
                var account = await _database.QuerySingleOrDefaultAsync<string>(
                    UserSql.GetUserAccontById,
                    ct,
                    new
                    {
                        userId = userId
                    });

                return account;
            }
            catch (Exception ex)
            {
                throw new Exception("Ошиюка подключения к базе данных");
            }
        }


        public async Task<string> GetUserAccountByLogin(string login, CancellationToken ct)
        {
            var delegateeAccount = await _database.QuerySingleOrDefaultAsync<string>(
                   UserSql.GetUserAccountByLogin,
                   ct,
                   new
                   {
                       login = login
                   });

            return delegateeAccount;
        }

        public async Task<int> GetUserIdByAccount(string account, CancellationToken ct)
        {
            var userId = await _database.QuerySingleOrDefaultAsync<int>(
               UserSql.GetUserIdByAccount,
               ct,
               new
               {
                   account = account
               });

            return userId;
        }
    }
}
