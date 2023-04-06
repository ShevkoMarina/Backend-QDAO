namespace QDAO.Domain
{
    public class Proposal
    {
        public uint Id { get; set; }

        public uint Proposer { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public uint Eta { get; set; }

        public uint StartBlock { get; set; }

        public uint EndBlock { get; set; }

        public uint ForVotes { get; set; }

        public uint AgainstVotes { get; set; }

        public bool Canceled { get; set; }

        public bool Executed { get; set; }
    }
}
