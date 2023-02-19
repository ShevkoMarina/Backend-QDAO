using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;
using BackendQDAO.Contracts.GovernorEvents.ContractDefinition;

namespace BackendQDAO.Contracts.GovernorEvents
{
    public partial class GovernorEventsService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, GovernorEventsDeployment governorEventsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<GovernorEventsDeployment>().SendRequestAndWaitForReceiptAsync(governorEventsDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, GovernorEventsDeployment governorEventsDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<GovernorEventsDeployment>().SendRequestAsync(governorEventsDeployment);
        }

        public static async Task<GovernorEventsService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, GovernorEventsDeployment governorEventsDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, governorEventsDeployment, cancellationTokenSource);
            return new GovernorEventsService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public GovernorEventsService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }


    }
}
