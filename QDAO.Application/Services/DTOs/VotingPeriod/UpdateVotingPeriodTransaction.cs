using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.Services.DTOs.VotingPeriod
{
    [Function("updateVotingPeriod")]
    public class UpdateVotingPeriodTransaction : FunctionMessage
    {
        [Parameter("uint256", "_newValue", 1)]
        public virtual BigInteger NewValue { get; set; }
    }
}
