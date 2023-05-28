namespace QDAO.Domain
{
    public enum ProposalState : short
    {
        Pending = 0,
        Active = 1,
        Canceled = 2,
        Defeated = 3,
        NoQuorum = 4,
        Succeeded = 5,
        Queued = 6,
        ReadyToExecute = 7,
        Executed = 8
    }
}
