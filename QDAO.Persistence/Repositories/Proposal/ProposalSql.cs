using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.Proposal
{
    internal static class ProposalSql
    {
        internal const string Add = @"--ProposalSql.Add
                                      insert into proposal
                                      (id, start_block, end_block, proposer_id) 
                                      values(@id, @start_block, @end_block, @proposer_id)";



        internal const string InsertState = @"--ProposalSql.InsertState
                                      insert into proposal_state_log
                                      (proposal_id, state) 
                                      values(@proposal_id, @state)";

    }
}
