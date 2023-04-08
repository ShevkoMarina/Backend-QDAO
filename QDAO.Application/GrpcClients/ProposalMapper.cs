using QDAO.Application.GrpcClients.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients
{
    public static class ProposalMapper
    {
        public static Domain.Proposal ToDomain(this ProposalDto proposalDto)
        {
            return new Domain.Proposal()
            {

            };
        }
    }
}
