using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public class GetUpdatableSettingsValues
    {
        public record Request() : IRequest<UpdatableSettingsInfo>;
        public record UpdatableSettingsInfo(long Quorum, long VotingPeriod, long VotingDelay);

        public class Handler : IRequestHandler<Request, UpdatableSettingsInfo>
        {
            private readonly ContractsManager _manager;
            public Handler(ContractsManager manager)
            {
                _manager = manager;
            }

            public async Task<UpdatableSettingsInfo> Handle(Request request, CancellationToken cancellationToken)
            {
                var getVotingPeriodHandler = _manager.Web3.Eth.GetContractQueryHandler<GetVotingPeriod>();
                var votingPeriod = await getVotingPeriodHandler.QueryAsync<long>(_manager.GetGovernorDelegator(), new GetVotingPeriod());

                var getVotingDelayHandler = _manager.Web3.Eth.GetContractQueryHandler<GetVotingDelay>();
                var votingDelay = await getVotingDelayHandler.QueryAsync<long>(_manager.GetGovernorDelegator(), new GetVotingDelay());

                var getQuorumHandler = _manager.Web3.Eth.GetContractQueryHandler<GetQuorumNumerator>();
                var quorum = await getQuorumHandler.QueryAsync<long>(_manager.GetGovernorDelegator(), new GetQuorumNumerator());

                return new UpdatableSettingsInfo(quorum, votingPeriod, votingDelay);
            }
        }


        [Function("votingPeriod", "uint256")]
        public class GetVotingPeriod : FunctionMessage {}

        [Function("votingDelay", "uint256")]
        public class GetVotingDelay : FunctionMessage { }

        [Function("quorumNumerator", "uint256")]
        public class GetQuorumNumerator : FunctionMessage { }
    }
}
