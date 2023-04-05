using Nethereum.Contracts;
using QDAO.Application.GrpcClients.DTOs.Admin;
using QDAO.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.GrpcClients
{
    public class AdminGrpcClient
    {
        private readonly ContractsManager _contractsManager;
        private readonly TransactionCreator _transactionService;

        private readonly string _governorAddress;

        public AdminGrpcClient(ContractsManager contractsManager, 
            TransactionCreator transactionService)
        {
            _contractsManager = contractsManager;
            _transactionService = transactionService;

            _governorAddress = _contractsManager.GetGovernorDelegator();
        }

        public async Task InitializeGovernor()
        {
            var defaultTx = await _transactionService.GetDefaultRawTransaction("0x618E0fFEe21406f493D22f9163c48E2D036de6B0");

            var request = new InitializeGovernorMessage
            {
                TimelockAddress = _contractsManager.GetTimelockAddress(),
                TokenAddress = _contractsManager.GetTokenAddress(),
                VotingPeriod = 2
            };

            defaultTx.Data = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(request.GetCallData());

        }
    }
}
