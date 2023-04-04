using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.Events;
using QDAO.Persistence.Repositories.Proposal;
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
        // Репка с обработкой транзакционных хешей
        public EventsPipeline(ContractsManager manager)
        {
            _manager = manager;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
           
            var txHash = "";
            var receipt = await _manager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);

            var proposalCreationEvents = receipt.DecodeAllEvents<ProposalCreatedEventDto>();
          

            
        }
    }
}
