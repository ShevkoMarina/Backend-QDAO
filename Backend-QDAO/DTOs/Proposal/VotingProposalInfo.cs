using QDAO.Domain;

namespace QDAO.Endpoint.DTOs.Proposal
{
    public class VotingProposalInfo
    {
        public long ProposalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public VotingInfo VotingInfo { get; set; }
    }
}
