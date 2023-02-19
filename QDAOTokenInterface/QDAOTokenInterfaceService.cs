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
using BackendQDAO.Contracts.QDAOTokenInterface.ContractDefinition;

namespace BackendQDAO.Contracts.QDAOTokenInterface
{
    public partial class QDAOTokenInterfaceService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QDAOTokenInterfaceDeployment qDAOTokenInterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTokenInterfaceDeployment>().SendRequestAndWaitForReceiptAsync(qDAOTokenInterfaceDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QDAOTokenInterfaceDeployment qDAOTokenInterfaceDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTokenInterfaceDeployment>().SendRequestAsync(qDAOTokenInterfaceDeployment);
        }

        public static async Task<QDAOTokenInterfaceService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QDAOTokenInterfaceDeployment qDAOTokenInterfaceDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, qDAOTokenInterfaceDeployment, cancellationTokenSource);
            return new QDAOTokenInterfaceService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public QDAOTokenInterfaceService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GetPastVotesQueryAsync(GetPastVotesFunction getPastVotesFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GetPastVotesFunction, BigInteger>(getPastVotesFunction, blockParameter);
        }

        
        public Task<BigInteger> GetPastVotesQueryAsync(string account, BigInteger blockNumber, BlockParameter blockParameter = null)
        {
            var getPastVotesFunction = new GetPastVotesFunction();
                getPastVotesFunction.Account = account;
                getPastVotesFunction.BlockNumber = blockNumber;
            
            return ContractHandler.QueryAsync<GetPastVotesFunction, BigInteger>(getPastVotesFunction, blockParameter);
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
