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
using BackendQDAO.Contracts.QDAOTokenV0Interface.ContractDefinition;

namespace BackendQDAO.Contracts.QDAOTokenV0Interface
{
    public partial class QDAOTokenV0InterfaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QDAOTokenV0InterfaceDeployment qDAOTokenV0InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTokenV0InterfaceDeployment>().SendRequestAndWaitForReceiptAsync(qDAOTokenV0InterfaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QDAOTokenV0InterfaceDeployment qDAOTokenV0InterfaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTokenV0InterfaceDeployment>().SendRequestAsync(qDAOTokenV0InterfaceDeployment);
        }

        public static async Task<QDAOTokenV0InterfaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QDAOTokenV0InterfaceDeployment qDAOTokenV0InterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, qDAOTokenV0InterfaceDeployment, cancellationTokenSource);
            return new QDAOTokenV0InterfaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public QDAOTokenV0InterfaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetCurrentVotesQueryAsync(GetCurrentVotesFunction getCurrentVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetCurrentVotesFunction, BigInteger>(getCurrentVotesFunction, blockParameter);
        }

        
        public Task<BigInteger> GetCurrentVotesQueryAsync(string account, BlockParameter blockParameter = null)
        {
            var getCurrentVotesFunction = new GetCurrentVotesFunction();
                getCurrentVotesFunction.Account = account;
            
            return ContractHandler.QueryAsync<GetCurrentVotesFunction, BigInteger>(getCurrentVotesFunction, blockParameter);
        }

        public Task<BigInteger> TotalSupplyQueryAsync(TotalSupplyFunction totalSupplyFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(totalSupplyFunction, blockParameter);
        }

        
        public Task<BigInteger> TotalSupplyQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TotalSupplyFunction, BigInteger>(null, blockParameter);
        }
    }
}
