using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients.DTOs.Admin
{
    [Function("initialize")]
    public class InitializeGovernorMessage : FunctionMessage
    {
        [Parameter("address", "_timelock", 1)]
        public string TimelockAddress { get; set; }

        [Parameter("address", "_token", 2)]
        public string TokenAddress { get; set; }

        [Parameter("uint256", "_votingPeriod", 3)]
        public BigInteger VotingPeriod { get; set; }

    }
}
