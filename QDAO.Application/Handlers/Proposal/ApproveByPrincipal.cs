using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public class ApproveByPrincipal
    {
        public record Request(long ProposalId, int UserId) : IRequest<RawTransaction>;

        public class Handler : IRequestHandler<Request, RawTransaction>
        {
            private TransactionCreator _transactionService;
            private UserRepository _userRespository;

            public Handler(TransactionCreator transactionService, UserRepository userRespository)
            {
                _transactionService = transactionService;
                _userRespository = userRespository;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken cancellationToken)
            {
                var userAccount = await _userRespository.GetUserAccountById(request.UserId, cancellationToken);
                var txMessage = new ApproveMessage
                {
                    ProposalId = request.ProposalId,

                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;


                return rawTx;
            }
        }
    }


    [Function("approve")]
    public class ApproveMessage : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public BigInteger ProposalId { get; set; }
    }
}
