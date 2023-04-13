namespace QDAO.Domain
{
    public record ProposalThin(long Id, string Name, string Description, ProposalState State);
}
