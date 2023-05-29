using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public static class SetPendingImplementationTxQuery
    {
        public record Request(int UserId, string ImplementationAddress) : IRequest<RawTransaction>;
        public class Handler : IRequestHandler<Request, RawTransaction>
        {
            private readonly UserRepository _userRepository;
            private readonly TransactionCreator _transactionService;

            public Handler(UserRepository userRepository, TransactionCreator transactionService)
            {
                _userRepository = userRepository;
                _transactionService = transactionService;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken cancellationToken)
            {
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var txMessage = new SetPendingTransaction
                {
                    PendingImplementation = request.ImplementationAddress,
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;

                return rawTx;
            }
        }

        [Function("setPendingImplementation")]
        public class SetPendingTransaction : FunctionMessage
        {
            [Parameter("address", "_pendingImplementation", 1)]
            public string PendingImplementation { get; set; }
        }
    }
}
