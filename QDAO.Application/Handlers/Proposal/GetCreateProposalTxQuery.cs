using MediatR;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Persistence.Repositories.User;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Application.Handlers.Proposal
{
    public static class GetCreateProposalTxQuery
    {
        public record Request(
            string Name,
            string Description,
            ProposalType Type,
            int UserId,
            int NewValue
            ) : IRequest<Response>;

        public record Response(RawTransaction RawTransaction);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly TransactionCreator _transactionService;
            private readonly ContractsManager _contractsManager;
            private readonly UserRepository _userRepository;

            public Handler(
                TransactionCreator transactionService,
                UserRepository userRepository,
                ContractsManager contractsManager)
            {
                _transactionService = transactionService;
                _contractsManager = contractsManager;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Request request, CancellationToken ct)
            {
                var userAccount = await _userRepository.GetUserAccountById(request.UserId, ct);
                var callData = GetProposalCalldata(request.Type, request.NewValue);

                var txMessage = new CreateProposalTransaction
                {
                    CalldatasForTx = new List<byte[]> { callData },
                    Values = new List<long>() { 0 },
                    Targets = new List<string>() { _contractsManager.GetGovernorDelegator() },
                    Description = request.Description,
                    Name = request.Name
                };

                var dataHex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(txMessage.GetCallData());

                var rawTx = await _transactionService.GetDefaultRawTransaction(userAccount);

                rawTx.Data = dataHex;


                return new Response(rawTx);
            }
        }


        private static byte[] GetProposalCalldata(ProposalType proposalType, long newValue)
        {

            switch (proposalType)
            {
                case ProposalType.UpdateVotingPeriod:
                    var updateVotingMessage = new UpdateVotingPeriodTransaction
                    {
                        NewValue = newValue
                    };

                    return updateVotingMessage.GetCallData();
                case ProposalType.UpdateQuorum:
                    var updateQuorum = new UpdateQuorumMessage 
                    {
                        NewValue = newValue
                    };

                    return updateQuorum.GetCallData();
                case ProposalType.UpdateVotingDelay:
                    var updateVotingDelay = new UpdateVotingDelayMessage
                    {
                        NewValue = newValue
                    };
                    return updateVotingDelay.GetCallData();
            }

            return Array.Empty<byte>();
        }



        [Function("createProposal", "uint256")]
        public class CreateProposalTransaction : FunctionMessage
        {
            [Parameter("address[]", "targets", 1)]
            public List<string> Targets { get; set; }
            [Parameter("uint256[]", "values", 2)]
            public List<long> Values { get; set; } 
            [Parameter("bytes[]", "calldatas", 3)]
            public List<byte[]> CalldatasForTx { get; set; }
            [Parameter("string", "name", 4)]
            public string Name { get; set; }

            [Parameter("string", "description", 5)]
            public string Description { get; set; }
        }


        [Function("updateVotingPeriod")]
        public class UpdateVotingPeriodTransaction : FunctionMessage
        {
            [Parameter("uint256", "_newValue", 1)]
            public virtual long NewValue { get; set; }
        }

        [Function("updateQuorum")]
        public class UpdateQuorumMessage : FunctionMessage
        {
            [Parameter("uint256", "_newValue", 1)]
            public virtual long NewValue { get; set; }
        }

        [Function("updateVotingDelay")]
        public class UpdateVotingDelayMessage : FunctionMessage
        {
            [Parameter("uint256", "_newValue", 1)]
            public virtual long NewValue { get; set; }
        }
    }
}
