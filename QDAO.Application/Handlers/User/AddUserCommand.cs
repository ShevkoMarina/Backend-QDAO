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
    public class AddUserCommand
    {
        public record Request(string login, string password, string account) : IRequest<Response>;

        public record Response(long UserId);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UserRepository _repository;
            private readonly IDapperExecutor _database;

            public Handler(IDapperExecutor database, UserRepository userRepository)
            {
                _repository = userRepository;
                _database = database;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                try
                {
                    var connection = await _database.OpenConnectionAsync(cancellationToken);

                    var userId = await _repository.AddUser(
                        request.login,
                        request.password,
                        request.account,
                        Roles.User,
                        connection,
                        cancellationToken);

                    return new Response(userId);
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
        }
    }
}
