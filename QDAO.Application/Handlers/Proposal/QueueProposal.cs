using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public class QueueProposal
    {
        public record Request(long ProposalId, int UserId) : IRequest<RawTransaction>;

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

                if (userAccount == default)
                {
                    throw new ArgumentException("Пользователь не найден");
                }

                var txMessage = new QueueMessage
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

    [Function("queueProposal")]
    public class QueueMessage : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public BigInteger ProposalId { get; set; }
    }
}
