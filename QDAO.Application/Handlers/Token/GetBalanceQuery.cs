using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public class GetBalanceQuery
    {
        public record Request(string Account) : IRequest<Response>;

        public record Response(uint Balance);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ContractsManager _contractManager;

            public Handler(ContractsManager contractsManager)
            {
                _contractManager = contractsManager;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var txMessage = new GetBalanceMessage
                {
                    Account = request.Account,
                };

                var handler = _contractManager.Web3.Eth.GetContractQueryHandler<GetBalanceMessage>();

                var count = await handler.QueryAsync<BigInteger>(_contractManager.GetTokenAddress(), txMessage);

                return new Response(((uint)count));
            }
        }
    }

    [Function("balanceOf", "uint256")]
    public class GetBalanceMessage : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public string Account { get; set; }
    }
}
