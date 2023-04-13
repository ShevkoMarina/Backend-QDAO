﻿using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.User
{
    public class AddAdminCommand
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
                var connection = await _database.OpenConnectionAsync(cancellationToken);

                // Добавить проверку что админ уже есть

                var userId = await _repository.AddUser(
                    request.login,
                    request.password,
                    request.account,
                    Roles.Admin,
                    connection,
                    cancellationToken);

                return new Response(userId);
            }
        }
    }
}
