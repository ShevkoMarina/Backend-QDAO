using MediatR;
using Npgsql;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.User
{
    public static class AddAdminCommand
    {
        public record Request(string Login, string Password, string Account) : IRequest;

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IDapperExecutor _database;

            public Handler(IDapperExecutor database)
            {
                _database = database;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var connection = await _database.OpenConnectionAsync(cancellationToken);

                    await _database.ExecuteAsync(
                        UpdateAdminAccount,
                        connection,
                        cancellationToken,
                        new
                        {
                            account = request.Account,
                            password = request.Password,
                            login = request.Login
                        });

                    return new Unit();
                }
                catch (NpgsqlException ex)
                {
                    if (ex.SqlState == "23505")
                    {
                        throw new ArgumentException("Данный логин уже занят");
                    }

                    throw new ArgumentException("Ошибка при работе с базой данных");
                }
                catch (Exception ex)
                {
                    throw new Exception("Ошибка подключения к база данных");
                }
            }

            private const string UpdateAdminAccount = @"update users set 
                                                        account = @account,
                                                        login = @login,
                                                        password = @password
                                                        where role = 3;";
        }
    }
}
