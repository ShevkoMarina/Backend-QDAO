using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.Web3;
using QDAO.Application.Services;
using QDAO.Application.Services.DTOs.VotingPeriod;
using QDAO.Application.Utils;
using QDAO.Domain;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class CreateProposalTxQuery
    {
        public record Request(
            string Name,
            string Description,
            ProposalType Type,
            long NewValue
            ) : IRequest<Response>;

        public record Response(RawTransaction RawTransaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TransactionCreator _transactionService;
            private readonly ContractsManager _contractsManager;

            public Handler(
                TransactionCreator transactionService,
                ContractsManager contractsManager)
            {
                _transactionService = transactionService;
                _contractsManager = contractsManager;
            }

            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
               
                var callDatas = new List<byte[]>();
                callDatas.Add(GetProposalCalldata(request.Type));

                var txMessage = new CreateProposalTransaction
                {
                    CalldatasForTx = callDatas,
                    Values = new List<BigInteger>() { 0 },
                    Targets = new List<string>() { _contractsManager.GetGovernorDelegator() }
                };

              
                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction("0x618E0fFEe21406f493D22f9163c48E2D036de6B0");

                rawTx.Data = dataHex;


                return new Response(rawTx);
            }
        }

        private static byte[] GetProposalCalldata(ProposalType proposalType)
        {
            if (proposalType == ProposalType.UpdateVotingPeriod)
            {
                var updateVotingMessage = new UpdateVotingPeriodTransaction
                {
                    NewValue = 5
                };

                return updateVotingMessage.GetCallData();
            }

            return Array.Empty<byte>();
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