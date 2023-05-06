using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Domain
{
    public enum ProposalType : short
    {
        Unknown = 0,
        UpdateVotingPeriod = 1,
        UpdateQuorum = 2
    }
}
