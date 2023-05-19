using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public class DelegateVotesQuery
    {
        public record Request(int UserId, string DelegateeLogin) : IRequest<RawTransaction>;

        public class Handler : IRequestHandler<Request, RawTransaction>
        {
            private readonly TransactionCreator _transactionCreator;
            private readonly IDapperExecutor _database;
            private readonly UserRepository _userRepository;

            public Handler(TransactionCreator transactionCreator, IDapperExecutor database, UserRepository userRepository)
            {
                _transactionCreator = transactionCreator;
                _database = database;
                _userRepository = userRepository;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken cancellationToken)
            {

                var delegateeAccount = await _database.QuerySingleOrDefaultAsync<string>(
                    GetUserAccountByLogin,
                    cancellationToken,
                    new
                    {
                        login = request.DelegateeLogin
                    });

                var userAccount = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var txMessage = new DelegateVotesMessage
                {
                    Delegatee = delegateeAccount
                };
                
                
                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var transaction = await _transactionCreator.GetDefaultRawTransaction(userAccount);

                transaction.Data = dataHex;
                return transaction;
            }

            private const string GetUserAccountByLogin = @"select account from users where login = @login;";

        }
    }

    [Function("delegate")]
    public class DelegateVotesMessage : FunctionMessage
    {
        [Parameter("address", "delegatee", 1)]
        public string Delegatee { get; set; }
    }
}
