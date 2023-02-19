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
using BackendQDAO.Contracts.QDAOGovernorDelegateStorageV1.ContractDefinition;

namespace BackendQDAO.Contracts.QDAOGovernorDelegateStorageV1
{
    public partial class QDAOGovernorDelegateStorageV1Service
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegateStorageV1Deployment qDAOGovernorDelegateStorageV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOGovernorDelegateStorageV1Deployment>().SendRequestAndWaitForReceiptAsync(qDAOGovernorDelegateStorageV1Deployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegateStorageV1Deployment qDAOGovernorDelegateStorageV1Deployment)
        {
            return web3.Eth.GetContractDeploymentHandler<QDAOGovernorDelegateStorageV1Deployment>().SendRequestAsync(qDAOGovernorDelegateStorageV1Deployment);
        }

        public static async Task<QDAOGovernorDelegateStorageV1Service> DeployContractAndGetServiceAsync(Nethereum.Web3.Web3 web3, QDAOGovernorDelegateStorageV1Deployment qDAOGovernorDelegateStorageV1Deployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, qDAOGovernorDelegateStorageV1Deployment, cancellationTokenSource);
            return new QDAOGovernorDelegateStorageV1Service(web3, receipt.ContractAddress);
        }

        protected Nethereum.Web3.Web3 Web3{ get; }

        public ContractHandler ContractHandler { get; }

        public QDAOGovernorDelegateStorageV1Service(Nethereum.Web3.Web3 web3, string contractAddress)
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

        public Task<BigInteger> ProposalCountQueryAsync(ProposalCountFunction proposalCountFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalCountFunction, BigInteger>(proposalCountFunction, blockParameter);
        }

        
        public Task<BigInteger> ProposalCountQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<ProposalCountFunction, BigInteger>(null, blockParameter);
        }

        public Task<ProposalsOutputDTO> ProposalsQueryAsync(ProposalsFunction proposalsFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<ProposalsFunction, ProposalsOutputDTO>(proposalsFunction, blockParameter);
        }

        public Task<ProposalsOutputDTO> ProposalsQueryAsync(BigInteger returnValue1, BlockParameter blockParameter = null)
        {
            var proposalsFunction = new ProposalsFunction();
                proposalsFunction.ReturnValue1 = returnValue1;
            
            return ContractHandler.QueryDeserializingToObjectAsync<ProposalsFunction, ProposalsOutputDTO>(proposalsFunction, blockParameter);
        }

        public Task<BigInteger> QuorumNumeratorQueryAsync(QuorumNumeratorFunction quorumNumeratorFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<QuorumNumeratorFunction, BigInteger>(quorumNumeratorFunction, blockParameter);
        }

        
        public Task<BigInteger> QuorumNumeratorQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<QuorumNumeratorFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> TimelockQueryAsync(TimelockFunction timelockFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimelockFunction, string>(timelockFunction, blockParameter);
        }

        
        public Task<string> TimelockQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TimelockFunction, string>(null, blockParameter);
        }

        public Task<string> TokenQueryAsync(TokenFunction tokenFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(tokenFunction, blockParameter);
        }

        
        public Task<string> TokenQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<TokenFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> VotingPeriodQueryAsync(VotingPeriodFunction votingPeriodFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingPeriodFunction, BigInteger>(votingPeriodFunction, blockParameter);
        }

        
        public Task<BigInteger> VotingPeriodQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<VotingPeriodFunction, BigInteger>(null, blockParameter);
        }
    }
}
