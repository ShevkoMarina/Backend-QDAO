using MediatR;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.Events;
using QDAO.Persistence.Repositories.Transaction;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Transaction
{
    public class GetEventsByTransactionHashQuery
    {
        public record Request(string TxHash) : IRequest<Response>;

        public record Response(List<EventLog<ProposalCreatedEventDto>> Events);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ContractsManager _contractsManager;
            private readonly TransactionRepository _transactionRepository;

            public Handler(ContractsManager contractsManager,
                TransactionRepository transactionRepository)
            {
                _contractsManager = contractsManager;
                _transactionRepository = transactionRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var txHash = request.TxHash;
                if (request.TxHash == null)
                {
                    var txInfo = await _transactionRepository.GetNext(cancellationToken);
                    txHash = txInfo.Hash;
                }

                var receipt = await _contractsManager.Web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(txHash);

                var events = new List<EventLog<ProposalCreatedEventDto>>();
                if (receipt != null)
                {
                    events = receipt.DecodeAllEvents<ProposalCreatedEventDto>();
                }

                return new Response(events);
            }
        }
    }
}
