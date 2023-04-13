using QDAO.Domain;
using QDAO.Endpoint.DTOs.Proposal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QDAO.Endpoint.DTOs.Proposal
{
    public class CreateProposalDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProposalType Type { get; set; }
        public long NewValue { get; set; }
        public long UserId { get; set; }
    }
}
