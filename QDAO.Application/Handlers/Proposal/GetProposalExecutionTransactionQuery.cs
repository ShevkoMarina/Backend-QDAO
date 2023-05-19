using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetProposalExecutionTransactionQuery
    {
        public record Request(long ProposalId, int UserId) : IRequest<RawTransaction>;

        public sealed class Handler : IRequestHandler<Request, RawTransaction>
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

                var txMessage = new ExecuteMessage
                {
                    ProposalId = request.ProposalId,
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;

                return rawTx;
            }
        }

        [Function("executeProposal")]
        private class ExecuteMessage : FunctionMessage
        {
            [Parameter("uint256", "proposalId", 1)]
            public long ProposalId { get; set; }
        }
    }
}
