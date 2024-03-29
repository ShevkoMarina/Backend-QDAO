﻿using QDAO.Domain;


namespace QDAO.Endpoint.DTOs.Proposal
{
    public class CreateProposalDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProposalType Type { get; set; }
        public int NewValue { get; set; }
        public int UserId { get; set; }
    }
}
