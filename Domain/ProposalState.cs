using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Domain
{
    public enum ProposalState : short
    {
        Active = 0,
        Canceled = 1,
        Defeated = 2,
        NoQuorum = 3,
        Succeeded = 4,
        Queued = 5,
        Expired = 6,
        Executed = 7
    }
}
