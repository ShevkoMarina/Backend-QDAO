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
                    return new PendingImplementationInfo(string.Empty, false, false);
                }

                var isPrincipalApprovedHandler = _manager.Web3.Eth.GetContractQueryHandler<isPrincipalApproved>();
                var isPrincipalApproved = await isPrincipalApprovedHandler.QueryAsync<bool>(_manager.GetGovernorDelegator(), new isPrincipalApproved());

                // Получаем все ли принципалы проголосовали
                var isChangeApprovedHandler = _manager.Web3.Eth.GetContractQueryHandler<isChangeApproved>();
                var isChangeApproved = await isChangeApprovedHandler.QueryAsync<bool>(_manager.GetGovernorDelegator(), new isChangeApproved());


                return new PendingImplementationInfo(pendingImplementation, isPrincipalApproved, isChangeApproved);
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
