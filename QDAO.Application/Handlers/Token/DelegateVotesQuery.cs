using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public class DelegateVotesQuery
    {
        public record Request(string Signer, string Delegatee) : IRequest<Response>;

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
                var txMessage = new DelegateVotesMessage
                {
                    Delegatee = request.Delegatee,
                };
                
                
                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var transaction = await _transactionCreator.GetDefaultRawTransaction(request.Signer);

                transaction.Data = dataHex;
                return new Response(transaction);
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
