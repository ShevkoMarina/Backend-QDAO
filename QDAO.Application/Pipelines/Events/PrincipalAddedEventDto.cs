using Nethereum.ABI.FunctionEncoding.Attributes;

namespace QDAO.Application.Pipelines.Events
{
    [Event("PrincipalAdded")]
    public class PrincipalAddedEventDto : IEventDTO
    {

        [Parameter("address", "_account", 1)]
        public string Principal { get; set; }
    }
}
