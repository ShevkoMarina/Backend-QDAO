using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
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
            private readonly UserRepository _userRepository;

            public Handler(TransactionCreator transactionCreator, UserRepository userRepository)
            {
                _transactionCreator = transactionCreator;
                _userRepository = userRepository;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken cancellationToken)
            {

                var delegateeAccount = await _userRepository.GetUserAccountByLogin(request.DelegateeLogin, cancellationToken);

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
        }
    }

    [Function("delegate")]
    public class DelegateVotesMessage : FunctionMessage
    {
        [Parameter("address", "delegatee", 1)]
        public string Delegatee { get; set; }
    }
}
