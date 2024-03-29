﻿using MediatR;
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
    public class VoteProposal 
    {
        public record Request(long ProposalId, bool Support, int UserId) : IRequest<RawTransaction>;

        public class Handler : IRequestHandler<Request, RawTransaction>
        {
            private readonly TransactionCreator _transactionService;
            private readonly UserRepository _userRepository;

            public Handler(
                TransactionCreator transactionService,
                UserRepository userRepository)
            {
                _transactionService = transactionService;
                _userRepository = userRepository;
            }

            public async Task<RawTransaction> Handle(Request request, CancellationToken ct)
            {
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, ct);

                var txMessage = new VoteTransaction
                {
                    ProposalId = request.ProposalId,
                    Support = request.Support

                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;

                return rawTx;
            }
        }
    }

    [Function("vote", "uint256")]
    public class VoteTransaction : FunctionMessage
    {
        [Parameter("uint256", "proposalId", 1)]
        public BigInteger ProposalId { get; set; }
        [Parameter("bool", "support", 2)]
        public bool Support { get; set; }
    }
}
