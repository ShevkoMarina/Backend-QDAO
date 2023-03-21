using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.ProposalQuorum
{
    public class ProposalQuorumQueueSql
    {
        internal const string GetNext = @"select * from proposal_quorum_crisis_queue
                                         where status = 0 limit 1";
    }
}
