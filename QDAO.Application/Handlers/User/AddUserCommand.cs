using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        }
    }
}
