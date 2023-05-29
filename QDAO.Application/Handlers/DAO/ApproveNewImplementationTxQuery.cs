using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.DAO
{
    public class ApproveNewImplementationTxQuery
    {
        public record Request(int UserId) : IRequest<RawTransaction>;

        public class Handler : IRequestHandler<Request, RawTransaction>
        {
            private TransactionCreator _transactionService;
            private readonly UserRepository _userRepository;

            public Handler(
                TransactionCreator transactionService,
                UserRepository userRepository)
            {
                _transactionService = transactionService;
                _userRepository = userRepository;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken cancellationToken)
            {
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, cancellationToken);

                var txMessage = new ApproveImplementationChange();
           
                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;

                return rawTx;
            }
        }
    }

    [Function("approveImplementationChange")]
    public class ApproveImplementationChange : FunctionMessage
    {

    }
}
