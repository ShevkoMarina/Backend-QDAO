using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public class ApproveByPrincipal
    {
        public record Request(uint ProposalId) : IRequest<Response>;

        public record Response(RawTransaction Transaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private TransactionCreator _transactionService;

            public Handler(TransactionCreator transactionService)
            {
                _transactionService = transactionService;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var txMessage = new ApproveMessage
                {
                    ProposalId = request.ProposalId,

                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction("0x618E0fFEe21406f493D22f9163c48E2D036de6B0");

                rawTx.Data = dataHex;


                return new Response(rawTx);
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
