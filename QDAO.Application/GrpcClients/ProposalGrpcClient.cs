using QDAO.Application.GrpcClients.DTOs;
using QDAO.Application.Services;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients
{
    public class ProposalGrpcClient
    {
        private readonly ContractsManager _contractsManager;
        private readonly string _governorAddress;

        public ProposalGrpcClient(
            ContractsManager contractsManager)
        {
            _contractsManager = contractsManager;

            _governorAddress = _contractsManager.GetGovernorAddress();
        }

        public async Task<ProposalDto> GetProposalById(long proposalId)
        {
            var request = new GetProposalByIdMessage
            {
                ProposalId = proposalId
            };

            var handler = _contractsManager.Web3.Eth.GetContractQueryHandler<GetProposalByIdMessage>();

            var proposal = await handler.QueryAsync<ProposalDto>(_governorAddress, request);

            return proposal;
        }
    }
}
