using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.Proposal
{
    internal static class ProposalSql
    {
        internal const string Add = @"insert into proposal
                                      (id, start_block, end_block, proposer_id) 
                                      values(@id, @start_block, @end_block, @proposer_id)";

    }
}
