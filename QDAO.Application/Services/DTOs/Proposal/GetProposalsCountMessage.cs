using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.Services.DTOs.Proposal
{
    [Function("proposalCount", "uint256")]
    public class GetProposalsCountMessage : FunctionMessage
    {

    }
}
