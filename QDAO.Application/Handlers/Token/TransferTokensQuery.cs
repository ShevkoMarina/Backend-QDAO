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
    public class TransferTokensQuery
    {
        public record Request(int UserId, string DelegateeLogin, long RowAmount) : IRequest<Response>;

        public record Response(RawTransaction TransactionData);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TransactionCreator _transactionCreator;
            private readonly UserRepository _userRepository;

            public Handler(TransactionCreator transactionCreator)
            {
                _transactionCreator = transactionCreator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var delegateeAccount = await _userRepository.GetUserAccountByLogin(request.DelegateeLogin, cancellationToken);
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var txMessage = new TransferTokensMessage
                {
                    DstAccount = delegateeAccount,
                    RowAmount = request.RowAmount
                };


                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());
                var transaction = await _transactionCreator.GetDefaultRawTransaction(userAccount);
                transaction.Data = dataHex;

                return new Response(transaction);
            }

            [Function("transfer", "bool")]
            public class TransferTokensMessage : FunctionMessage
            {
                [Parameter("address", "dst", 1)]
                public string DstAccount { get; set; }

                [Parameter("uint256", "rawAmount", 2)]
                public long RowAmount { get; set; }
            }
        }
    }
}
