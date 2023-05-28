namespace QDAO.Persistence.Repositories.Proposal
{
    internal static class ProposalSql
    {
        internal const string Add = @"--ProposalSql.Add
                                      insert into proposal
                                      (id, proposer_id, description, name) 
                                      values(@id, @proposer_id, @description, @name)";



        internal const string InsertState = @"--ProposalSql.InsertState
                                            insert into proposal_state_log
                                            (proposal_id, state) 
                                            values(@proposal_id, @state)";

        internal const string InsertVotingInfo = @"insert into voting 
                                            (proposal_id, start_block, end_block, votes_for, votes_against)
                                            values (@proposal_id, @start_block, @end_block, @votes_for, @votes_against)";

        internal const string AddVotesForProposal = @"update voting set 
                                                votes_for = @votes_for
                                                where proposal_id = @proposal_id;";

        internal const string AddVotesAgainstProposal = @"update voting set
                                                votes_against = @votes_against 
                                                where proposal_id = @proposal_id;";


        internal const string GetByProposer = @"--ProposalSql.GetByProposer
                                                select id,
                                                name,
                                                description,
                                                state
                                                from proposal p
                                                join lateral (
                                                    select state, created_at
                                                    from proposal_state_log psl
                                                    where psl.proposal_id = p.id
                                                    order by psl.created_at DESC NULLS LAST
                                                    limit 1
                                                    ) AS proposal_state on true
                                                where proposer_id = @proposer_id;";

    }
}
