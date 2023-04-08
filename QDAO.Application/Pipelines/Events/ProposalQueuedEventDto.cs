using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines.Events
{
    [Event("ProposalQueued")]
    public class ProposalQueuedEventDto : IEventDTO
    {
        [Parameter("uint256", "id", 1)]
        public uint Id { get; set; }

        [Parameter("uint256", "eta", 2)]
        public uint Eta { get; set; }
    }
}
