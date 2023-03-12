using Nethereum.ABI.FunctionEncoding.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients.DTOs
{
    [FunctionOutput]
    public class ProposalDto : IFunctionOutputDTO
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }


        [Parameter("address", "proposer", 2)]
        public virtual string Proposer { get; set; }


        [Parameter("uint256", "eta", 3)]
        public virtual BigInteger Eta { get; set; }


        [Parameter("uint256", "startBlock", 4)]
        public virtual BigInteger StartBlock { get; set; }


        [Parameter("uint256", "endBlock", 5)]
        public virtual BigInteger EndBlock { get; set; }


        [Parameter("uint256", "forVotes", 6)]
        public virtual BigInteger ForVotes { get; set; }


        [Parameter("uint256", "againstVotes", 7)]
        public virtual BigInteger AgainstVotes { get; set; }


        [Parameter("bool", "canceled", 8)]
        public virtual bool Canceled { get; set; }


        [Parameter("bool", "executed", 9)]
        public virtual bool Executed { get; set; }
    }
}
