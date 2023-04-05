using MediatR;
using QDAO.Application.Services;
using QDAO.Persistence;
using QDAO.Persistence.Repositories.Transaction;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Transaction
{
    public class ExecuteTransactionCommand
    {
        public record Request(string TxData) : IRequest<Response>;

        public record Response(string TxHash);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly ContractsManager _contractdManager;
            private readonly TransactionRepository _transactionRepository;
            private readonly IDapperExecutor _database;

            public Handler(ContractsManager contractsManager,
                TransactionRepository transactionRepository,
                IDapperExecutor database)
            {
                _contractdManager = contractsManager;
                _transactionRepository = transactionRepository;
                _database = database;
            }

            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
                var txHash = await _contractdManager.Web3.Eth.Transactions.SendRawTransaction.SendRequestAsync(request.TxData);
               
                using var connection = await _database.OpenConnectionAsync(ct);
                await _transactionRepository.SaveTransaction(request.TxData, connection, ct);

                return new Response(txHash);
            }
        }
    }
}
