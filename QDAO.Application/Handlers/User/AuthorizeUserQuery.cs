using MediatR;
using QDAO.Persistence.Repositories.User;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.User
{
    public class AuthorizeUserQuery
    {
        public record Request(string login, string password) : IRequest<Response>;

        public record Response(UserInfoDto User);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly UserRepository _repository;

            public Handler(UserRepository repository)
            {
                _repository = repository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var user = await _repository.AuthorizeUser(request.login, request.password, cancellationToken);

                if (user != default)
                {
                    return new Response(user);
                }

                throw new ArgumentException("Пользователь не найден");
            }
        }
    }
}
