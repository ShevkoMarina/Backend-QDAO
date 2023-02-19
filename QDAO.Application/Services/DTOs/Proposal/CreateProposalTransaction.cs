using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System.Collections.Generic;
using System.Numerics;

namespace QDAO.Application.Services.DTOs.Proposal
{
    [Function("createProposal", "uint256")]
    public class CreateProposalTransaction : FunctionMessage
    {
        [Parameter("address[]", "targets", 1)]
        public virtual List<string> Targets { get; set; }
        [Parameter("uint256[]", "values", 2)]
        public virtual List<BigInteger> Values { get; set; }
        [Parameter("bytes[]", "calldatas", 3)]
        public virtual List<byte[]> Calldatas { get; set; }
        [Parameter("string", "description", 4)]
        public virtual string Description { get; set; }
    }
}
