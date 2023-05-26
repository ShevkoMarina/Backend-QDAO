using MediatR;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.User
{
    public class AddAdminCommand
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

            private const string UpdateAdminAccount = @"update users set 
                                                        account = @account,
                                                        login = @login,
                                                        password = @password
                                                        where role = 3;";
        }
    }
}
