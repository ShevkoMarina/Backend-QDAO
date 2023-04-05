using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.Events;
using QDAO.Persistence.Repositories.Proposal;
using QDAO.Persistence.Repositories.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Pipelines
{
    public class EventsPipeline
    {
        private readonly ContractsManager _manager;

        private readonly ProposalRepository _repository;
        private readonly TransactionRepository _transactionRepostiory;
        public EventsPipeline(ContractsManager manager)
        {
            _manager = manager;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
           
           // _transactionRepostiory
         //   var receipt = await _manager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);

          //  var proposalCreationEvents = receipt.DecodeAllEvents<ProposalCreatedEventDto>();
          

            
        }
    }
}
