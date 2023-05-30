namespace QDAO.Domain
{
    public class Proposal
    {
        public uint Id { get; set; }

        public int Proposer { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public uint Eta { get; set; }

        public VotingInterval VotingInterval { get; set; }

        public uint ForVotes { get; set; }

        public uint AgainstVotes { get; set; }

        public ProposalState State { get; set; }
    }
}