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
using BackendQDAO.Contracts.QDAOGovernorDelegatorStorage.ContractDefinition;

namespace BackendQDAO.Contracts.QDAOGovernorDelegatorStorage
{
    public partial class QDAOGovernorDelegatorStorageService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegatorStorageDeployment qDAOGovernorDelegatorStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOGovernorDelegatorStorageDeployment>().SendRequestAndWaitForReceiptAsync(qDAOGovernorDelegatorStorageDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegatorStorageDeployment qDAOGovernorDelegatorStorageDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOGovernorDelegatorStorageDeployment>().SendRequestAsync(qDAOGovernorDelegatorStorageDeployment);
        }

        public static async Task<QDAOGovernorDelegatorStorageService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegatorStorageDeployment qDAOGovernorDelegatorStorageDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, qDAOGovernorDelegatorStorageDeployment, cancellationTokenSource);
            return new QDAOGovernorDelegatorStorageService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public QDAOGovernorDelegatorStorageService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<string> AdminQueryAsync(AdminFunction adminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(adminFunction, blockParameter);
        }

        
        public Task<string> AdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(null, blockParameter);
        }

        public Task<string> ImplementationQueryAsync(ImplementationFunction implementationFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ImplementationFunction, string>(implementationFunction, blockParameter);
        }

        
        public Task<string> ImplementationQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ImplementationFunction, string>(null, blockParameter);
        }

        public Task<string> PendingAdminQueryAsync(PendingAdminFunction pendingAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingAdminFunction, string>(pendingAdminFunction, blockParameter);
        }

        
        public Task<string> PendingAdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingAdminFunction, string>(null, blockParameter);
        }
    }
}
