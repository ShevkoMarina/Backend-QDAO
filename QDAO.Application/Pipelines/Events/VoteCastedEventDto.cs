using Nethereum.ABI.FunctionEncoding.Attributes;

namespace QDAO.Application.Pipelines.Events
{
    [Event("VoteCasted")]
    public class VoteCastedEventDto : IEventDTO
    {
        [Parameter("address", "voter", 1, true)]
        public string Voter { get; set; }

        [Parameter("uint256", "proposalId", 2)]
        public long ProposalId { get; set; }

        [Parameter("bool", "support", 3)]
        public bool Support { get; set; }

        [Parameter("uint256", "votes", 4)]
        public long Votes { get; set; }
    }
}
