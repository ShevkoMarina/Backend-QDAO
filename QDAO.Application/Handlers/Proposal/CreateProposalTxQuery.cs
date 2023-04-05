﻿using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.VotingPeriod;
using QDAO.Application.Utils;
using QDAO.Domain;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class CreateProposalTxQuery
    {
        public record Request(string Name, string Description) : IRequest<Response>;

        public record Response(RawTransaction Transaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private TransactionCreator _transactionService;

            public Handler(TransactionCreator transactionService)
            {
                _transactionService = transactionService;
            }

            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
                var updateVotingMessage = new UpdateVotingPeriodTransaction
                {
                    NewValue = 5
                };

                var callDatas = new List<byte[]>();
                callDatas.Add(updateVotingMessage.GetCallData());

                var txMessage = new CreateProposalTransaction
                {
                    CalldatasForTx = callDatas,
                    Values = new List<BigInteger>() { 0 },
                    Targets = new List<string>() { "0x25B9a573399CF9D1E50fcdE89aB8782271531CeE" }
         
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction("0x618E0fFEe21406f493D22f9163c48E2D036de6B0");

                rawTx.Data = dataHex;


                return new Response(rawTx);
            }
        }

        

        [Function("createProposal", "uint256")]
        public class CreateProposalTransaction : FunctionMessage
        {
            [Parameter("address[]", "targets", 1)]
            public virtual List<string> Targets { get; set; }
            [Parameter("uint256[]", "values", 2)]
            public virtual List<BigInteger> Values { get; set; } 
            [Parameter("bytes[]", "calldatas", 3)]
            public virtual List<byte[]> CalldatasForTx { get; set; }
        }
    }
}