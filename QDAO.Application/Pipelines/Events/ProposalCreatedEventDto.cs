using Nethereum.ABI.FunctionEncoding.Attributes;
using System.Collections.Generic;

namespace QDAO.Application.Services.DTOs.Events
{
    [Event("ProposalCreated")]
    public class ProposalCreatedEventDto : IEventDTO
    {
        [Parameter("uint256", "id", 1)]
        public uint Id { get; set; }

        [Parameter("address", "proposer", 2)]
        public string Proposer { get; set; }

        [Parameter("address[]", "targets", 3)]
        public List<string> Targets { get; set; }

        [Parameter("uint[]", "values", 4)]
        public List<uint> Values { get; set; }

        [Parameter("bytes[]", "calldatas", 5)]
        public List<byte[]> Calldatas { get; set; }

        [Parameter("uint256", "startBlock", 6)]
        public uint StartBlock { get; set; }

        [Parameter("uint256", "endBlock", 7)]
        public uint EndBlock { get; set; }

        [Parameter("string", "name", 7)]
        public string Name { get; set; }

        [Parameter("string", "description", 8)]
        public string Description { get; set; }
    }
}
