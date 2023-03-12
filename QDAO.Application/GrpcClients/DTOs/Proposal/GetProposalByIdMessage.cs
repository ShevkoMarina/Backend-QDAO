using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients.DTOs
{
    [Function("proposals", typeof(ProposalDto))]
    class GetProposalByIdMessage : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ProposalId { get; set; }
    }
}
