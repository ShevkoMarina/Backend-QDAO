using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public class TransferTokensQuery
    {
        public record Request(string Signer, string DstAccount, uint RowAmount) : IRequest<Response>;

        public record Response(RawTransaction TransactionData);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TransactionCreator _transactionCreator;

            public Handler(TransactionCreator transactionCreator)
            {
                _transactionCreator = transactionCreator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var txMessage = new TransferTokensMessage
                {
                    DstAccount = request.DstAccount,
                    RowAmount = request.RowAmount
                };


                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());
                var transaction = await _transactionCreator.GetDefaultRawTransaction(request.Signer);
                transaction.Data = dataHex;

                return new Response(transaction);
            }

            [Function("transfer", "bool")]
            public class TransferTokensMessage : FunctionMessage
            {
                [Parameter("address", "dst", 1)]
                public string DstAccount { get; set; }

                [Parameter("uint256", "rawAmount", 2)]
                public uint RowAmount { get; set; }
            }
        }
    }
}
