namespace QDAO.Persistence.Repositories.User
{
    internal static class UserSql
    {
        internal const string Add = @"--UserSql.Add
                                    insert into users (login, account, password, role) values
                                    (@login, @account, @password, @role)
                                    returning id;";


        internal const string GetByIds = @"--UserSql.GetByIds
                                    select 
                                    id,
                                    login,
                                    password,
                                    account as address,
                                    role
                                    from users where id = @userId ;";

        internal const string GetUserAccontById = @"--UserSql.GetUserAccountById
                                                    select account from users where id = @userId";

        internal const string GetUserIdByAccount = @"--UserSql.GetUserIdByAccount
                                                    select id from users where account = @account";


        internal const string GetUserAccountByLogin = @"--GetUserAccountByLogin
                                                        select account from users
                                                        where login = @login;";

    }
}
