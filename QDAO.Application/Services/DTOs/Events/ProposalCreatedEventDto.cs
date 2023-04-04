using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.Services.DTOs.Events
{
    [Event("ProposalCreated")]
    public class ProposalCreatedEventDto : IEventDTO
    {
        [Parameter("uint256", "id", 1)]
        public BigInteger Id { get; set; }

        [Parameter("address", "proposer", 2)]
        public string Proposer { get; set; }

        [Parameter("address[]", "targets", 3)]
        public List<string> Targets { get; set; }

        [Parameter("uint[]", "values", 4)]
        public List<BigInteger> Values{ get; set; }

        [Parameter("bytes[]", "calldatas", 5)]
        public List<byte[]> Calldatas { get; set; }

        [Parameter("uint256", "startBlock", 6)]
        public BigInteger StartBlock { get; set; }

        [Parameter("uint256", "endBlock", 7)]
        public BigInteger EndBlock { get; set; }
    }
}
