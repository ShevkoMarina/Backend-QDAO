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
    public class EventsProcessingPipeline
    {
        private readonly ContractsManager _manager;
        private readonly ProposalRepository _repository;
        private readonly TransactionRepository _transactionRepostiory;
        private readonly TransactionEventsDecoder _transactionEventsDecoder;

        public EventsProcessingPipeline(ContractsManager manager,
            TransactionRepository transactionRepository,
            TransactionEventsDecoder transactionEventsDecoder)
        {
            _manager = manager;
            _transactionRepostiory = transactionRepository;
            _transactionEventsDecoder = transactionEventsDecoder;
        }

        public async Task PipeAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var transactionQueued = await _transactionRepostiory.GetNext(stoppingToken);

                if (transactionQueued is null)
                {
                    await Task.Delay(1000, stoppingToken);
                    break;
                }

                var receipt = await _manager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(transactionQueued.Hash);

                if (receipt is null)
                {
                    // 
                }

                var decodedEvents = _transactionEventsDecoder.Decode(receipt);

                var contractEvent = decodedEvents.First();

                if (contractEvent is ProposalCreatedEventDto)
                {
                    // Сохранить в БД
                }
            }
        }
    }
}
