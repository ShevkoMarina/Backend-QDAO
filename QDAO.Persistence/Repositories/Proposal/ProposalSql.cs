namespace QDAO.Persistence.Repositories.Proposal
{
    internal static class ProposalSql
    {
        internal const string Add = @"--ProposalSql.Add
                                      insert into proposal
                                      (id, start_block, end_block, proposer_id, description, name) 
                                      values(@id, @start_block, @end_block, @proposer_id, @description, @name)";



        internal const string InsertState = @"--ProposalSql.InsertState
                                            insert into proposal_state_log
                                            (proposal_id, state) 
                                            values(@proposal_id, @state)";


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
