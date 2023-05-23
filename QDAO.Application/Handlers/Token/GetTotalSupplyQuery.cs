using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Token
{
    public static class GetTotalSupplyQuery
    {
        public record Request() : IRequest<long>;

        public sealed class Handler : IRequestHandler<Request, long>
        {
            private readonly ContractsManager _contractManager;

            public Handler(ContractsManager contractManager)
            {
                _contractManager = contractManager;
            }

            public async Task<long> Handle(Request request, CancellationToken cancellationToken)
            {
                var getTotalSupplyHandler = _contractManager.Web3.Eth.GetContractQueryHandler<GetTotalSupplyMessage>();
                var totalSupply = await getTotalSupplyHandler.QueryAsync<long>(_contractManager.GetTokenAddress(), new GetTotalSupplyMessage());

                return totalSupply;
            }

            [Function("totalSupply", "uint256")]
            private class GetTotalSupplyMessage : FunctionMessage { }
        }
    }
}
