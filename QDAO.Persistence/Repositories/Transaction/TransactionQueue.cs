using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.Transaction
{
    public class TransactionQueue
    {
        public long QueueId { get; set; }
        public string Hash { get; set; }
    }
}
