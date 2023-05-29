using Nethereum.ABI.FunctionEncoding.Attributes;

namespace QDAO.Application.Pipelines.Events
{
    [Event("ProposalExecuted")]
    public class ProposalExecutedEventDto : IEventDTO
    {
        [Parameter("uint256", "id", 1)]
        public uint Id { get; set; }
    }
}
