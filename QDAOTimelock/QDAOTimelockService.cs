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
using BackendQDAO.Contracts.QDAOTimelock.ContractDefinition;

namespace BackendQDAO.Contracts.QDAOTimelock
{
    public partial class QDAOTimelockService
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QDAOTimelockDeployment qDAOTimelockDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTimelockDeployment>().SendRequestAndWaitForReceiptAsync(qDAOTimelockDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QDAOTimelockDeployment qDAOTimelockDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOTimelockDeployment>().SendRequestAsync(qDAOTimelockDeployment);
        }

        public static async Task<QDAOTimelockService> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QDAOTimelockDeployment qDAOTimelockDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, qDAOTimelockDeployment, cancellationTokenSource);
            return new QDAOTimelockService(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public QDAOTimelockService(Nethereum.Web3.Web3 web3, string contractAddress)
        {
            Web3 = web3;
            ContractHandler = web3.Eth.GetContractHandler(contractAddress);
        }

        public Task<BigInteger> GRACE_PERIODQueryAsync(GRACE_PERIODFunction gRACE_PERIODFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GRACE_PERIODFunction, BigInteger>(gRACE_PERIODFunction, blockParameter);
        }

        
        public Task<BigInteger> GRACE_PERIODQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<GRACE_PERIODFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> MAXIMUM_DELAYQueryAsync(MAXIMUM_DELAYFunction mAXIMUM_DELAYFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MAXIMUM_DELAYFunction, BigInteger>(mAXIMUM_DELAYFunction, blockParameter);
        }

        
        public Task<BigInteger> MAXIMUM_DELAYQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MAXIMUM_DELAYFunction, BigInteger>(null, blockParameter);
        }

        public Task<BigInteger> MINIMUM_DELAYQueryAsync(MINIMUM_DELAYFunction mINIMUM_DELAYFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MINIMUM_DELAYFunction, BigInteger>(mINIMUM_DELAYFunction, blockParameter);
        }

        
        public Task<BigInteger> MINIMUM_DELAYQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MINIMUM_DELAYFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> AcceptAdminRequestAsync(AcceptAdminFunction acceptAdminFunction)
        {
             return ContractHandler.SendRequestAsync(acceptAdminFunction);
        }

        public Task<string> AcceptAdminRequestAsync()
        {
             return ContractHandler.SendRequestAsync<AcceptAdminFunction>();
        }

        public Task<TransactionReceipt> AcceptAdminRequestAndWaitForReceiptAsync(AcceptAdminFunction acceptAdminFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(acceptAdminFunction, cancellationToken);
        }

        public Task<TransactionReceipt> AcceptAdminRequestAndWaitForReceiptAsync(CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync<AcceptAdminFunction>(null, cancellationToken);
        }

        public Task<string> AdminQueryAsync(AdminFunction adminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(adminFunction, blockParameter);
        }

        
        public Task<string> AdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<AdminFunction, string>(null, blockParameter);
        }

        public Task<string> CancelTransactionRequestAsync(CancelTransactionFunction cancelTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(cancelTransactionFunction);
        }

        public Task<TransactionReceipt> CancelTransactionRequestAndWaitForReceiptAsync(CancelTransactionFunction cancelTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelTransactionFunction, cancellationToken);
        }

        public Task<string> CancelTransactionRequestAsync(string target, BigInteger value, byte[] data, BigInteger eta)
        {
            var cancelTransactionFunction = new CancelTransactionFunction();
                cancelTransactionFunction.Target = target;
                cancelTransactionFunction.Value = value;
                cancelTransactionFunction.Data = data;
                cancelTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAsync(cancelTransactionFunction);
        }

        public Task<TransactionReceipt> CancelTransactionRequestAndWaitForReceiptAsync(string target, BigInteger value, byte[] data, BigInteger eta, CancellationTokenSource cancellationToken = null)
        {
            var cancelTransactionFunction = new CancelTransactionFunction();
                cancelTransactionFunction.Target = target;
                cancelTransactionFunction.Value = value;
                cancelTransactionFunction.Data = data;
                cancelTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(cancelTransactionFunction, cancellationToken);
        }

        public Task<BigInteger> DelayQueryAsync(DelayFunction delayFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DelayFunction, BigInteger>(delayFunction, blockParameter);
        }

        
        public Task<BigInteger> DelayQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<DelayFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> ExecuteTransactionRequestAsync(ExecuteTransactionFunction executeTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(executeTransactionFunction);
        }

        public Task<TransactionReceipt> ExecuteTransactionRequestAndWaitForReceiptAsync(ExecuteTransactionFunction executeTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeTransactionFunction, cancellationToken);
        }

        public Task<string> ExecuteTransactionRequestAsync(string target, BigInteger value, byte[] data, BigInteger eta)
        {
            var executeTransactionFunction = new ExecuteTransactionFunction();
                executeTransactionFunction.Target = target;
                executeTransactionFunction.Value = value;
                executeTransactionFunction.Data = data;
                executeTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAsync(executeTransactionFunction);
        }

        public Task<TransactionReceipt> ExecuteTransactionRequestAndWaitForReceiptAsync(string target, BigInteger value, byte[] data, BigInteger eta, CancellationTokenSource cancellationToken = null)
        {
            var executeTransactionFunction = new ExecuteTransactionFunction();
                executeTransactionFunction.Target = target;
                executeTransactionFunction.Value = value;
                executeTransactionFunction.Data = data;
                executeTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeTransactionFunction, cancellationToken);
        }

        public Task<string> PendingAdminQueryAsync(PendingAdminFunction pendingAdminFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingAdminFunction, string>(pendingAdminFunction, blockParameter);
        }

        
        public Task<string> PendingAdminQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<PendingAdminFunction, string>(null, blockParameter);
        }

        public Task<string> QueueTransactionRequestAsync(QueueTransactionFunction queueTransactionFunction)
        {
             return ContractHandler.SendRequestAsync(queueTransactionFunction);
        }

        public Task<TransactionReceipt> QueueTransactionRequestAndWaitForReceiptAsync(QueueTransactionFunction queueTransactionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(queueTransactionFunction, cancellationToken);
        }

        public Task<string> QueueTransactionRequestAsync(string target, BigInteger value, byte[] data, BigInteger eta)
        {
            var queueTransactionFunction = new QueueTransactionFunction();
                queueTransactionFunction.Target = target;
                queueTransactionFunction.Value = value;
                queueTransactionFunction.Data = data;
                queueTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAsync(queueTransactionFunction);
        }

        public Task<TransactionReceipt> QueueTransactionRequestAndWaitForReceiptAsync(string target, BigInteger value, byte[] data, BigInteger eta, CancellationTokenSource cancellationToken = null)
        {
            var queueTransactionFunction = new QueueTransactionFunction();
                queueTransactionFunction.Target = target;
                queueTransactionFunction.Value = value;
                queueTransactionFunction.Data = data;
                queueTransactionFunction.Eta = eta;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(queueTransactionFunction, cancellationToken);
        }

        public Task<bool> QueuedTransactionsQueryAsync(QueuedTransactionsFunction queuedTransactionsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<QueuedTransactionsFunction, bool>(queuedTransactionsFunction, blockParameter);
        }

        
        public Task<bool> QueuedTransactionsQueryAsync(byte[] returnValue1, BlockParameter blockParameter = null)
        {
            var queuedTransactionsFunction = new QueuedTransactionsFunction();
                queuedTransactionsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryAsync<QueuedTransactionsFunction, bool>(queuedTransactionsFunction, blockParameter);
        }

        public Task<string> SetPendingAdminRequestAsync(SetPendingAdminFunction setPendingAdminFunction)
        {
             return ContractHandler.SendRequestAsync(setPendingAdminFunction);
        }

        public Task<TransactionReceipt> SetPendingAdminRequestAndWaitForReceiptAsync(SetPendingAdminFunction setPendingAdminFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPendingAdminFunction, cancellationToken);
        }

        public Task<string> SetPendingAdminRequestAsync(string pendingAdmin_)
        {
            var setPendingAdminFunction = new SetPendingAdminFunction();
                setPendingAdminFunction.PendingAdmin_ = pendingAdmin_;
            
             return ContractHandler.SendRequestAsync(setPendingAdminFunction);
        }

        public Task<TransactionReceipt> SetPendingAdminRequestAndWaitForReceiptAsync(string pendingAdmin_, CancellationTokenSource cancellationToken = null)
        {
            var setPendingAdminFunction = new SetPendingAdminFunction();
                setPendingAdminFunction.PendingAdmin_ = pendingAdmin_;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setPendingAdminFunction, cancellationToken);
        }
    }
}
