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
        public  BigInteger Id { get; set; }

        [Parameter("address", "proposer", 2)]
        public string Proposer { get; set; }

        [Parameter("uint256", "eta", 3)]
        public  BigInteger Eta { get; set; }

        [Parameter("address[]", "targets", 4)]
        public List<string> Targets { get; set; }

        [Parameter("uint256[]", "values", 5)]
        public  List<BigInteger> Values { get; set; }
      //  [Parameter("bytes[]", "calldatas", 6)]
     //   public  List<byte[]> Calldatas { get; set; }

        [Parameter("uint256", "startBlock", 7)]
        public  BigInteger StartBlock { get; set; }

        [Parameter("uint256", "endBlock", 8)]
        public  BigInteger EndBlock { get; set; }
        [Parameter("uint256", "forVotes", 9)]
       public  BigInteger ForVotes { get; set; }

        [Parameter("uint256", "againstVotes", 10)]
        public  BigInteger AgainstVotes { get; set; }

        [Parameter("bool", "canceled", 11)]
        public  bool Canceled { get; set; }

        [Parameter("bool", "executed", 12)]
        public  bool Executed { get; set; }
    }
}
