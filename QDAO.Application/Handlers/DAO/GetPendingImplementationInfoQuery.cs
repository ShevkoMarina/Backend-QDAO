using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public static class GetPendingImplementationInfoQuery
    {
        public record Request() : IRequest<PendingImplementationInfo>;

        public record PendingImplementationInfo(string PendingAddress, bool isApproved, bool isChangeApproved);

        public class Handler : IRequestHandler<Request, PendingImplementationInfo>
        {
            private readonly ContractsManager _manager;
            public Handler(ContractsManager manager)
            {
                _manager = manager;
            }

            public async Task<PendingImplementationInfo> Handle(Request request, CancellationToken cancellationToken)
            {
                var getPendingImplementationHandler = _manager.Web3.Eth.GetContractQueryHandler<GetPendingImplementation>();
                var pendingImplementation = await getPendingImplementationHandler.QueryAsync<string>(_manager.GetGovernorDelegator(), new GetPendingImplementation());

                if (pendingImplementation == "0x0000000000000000000000000000000000000000")
                {
                    return new PendingImplementationInfo("0x0000000000000000000000000000000000000000", false, false);
                }

                return new PendingImplementationInfo(pendingImplementation, false, false);
            }
        }

        [Function("pendingImplementation", "address")]
        public class GetPendingImplementation : FunctionMessage { }

        [Function("isPrincipalApproved", "bool")]
        public class isPrincipalApproved : FunctionMessage { }

        [Function("isChangeApproved", "bool")]
        public class isChangeApproved : FunctionMessage { }
    }
}
