using Nethereum.ABI.FunctionEncoding.Attributes;

namespace QDAO.Application.Pipelines.Events
{
    [Event("ProposalApproved")]
    public class ProposalApprovedEventDto : IEventDTO
    {
        [Parameter("address", "approver", 1, true)]
        public string Voter { get; set; }

        [Parameter("uint256", "proposalId", 2)]
        public long ProposalId { get; set; }
    }
}
