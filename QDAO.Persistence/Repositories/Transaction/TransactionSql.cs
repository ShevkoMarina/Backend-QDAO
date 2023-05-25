using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Persistence.Repositories.Transaction
{
    internal static class TransactionSql
    {
        internal const string AddTransaction = @"insert into transactions_queue (hash) 
                                                 values(@tx_hash)";

        internal const string GetNext = @"select 
                                          id as QueueId, 
                                          hash as Hash 
                                          from transactions_queue
                                          where state = 0
                                          order by created_at
                                          limit 1";

        internal const string SetProccessed = @"update transactions_queue 
                                                set
                                                state = 1,
                                                processed_at = now()
                                                where id = @queue_id;";

        internal const string SetFailed = @"update transactions_queue 
                                            set
                                            state = 2,
                                            processed_at = now()
                                            where id = @queue_id;";
    }
}
