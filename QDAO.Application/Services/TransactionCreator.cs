using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using QDAO.Domain;
using System.Threading.Tasks;

namespace QDAO.Application.Services
{
    public class TransactionCreator
    {
        private readonly ContractsManager _contractsManager;

        public TransactionCreator(
            ContractsManager contractsManager)
        {
            _contractsManager = contractsManager;
        }

        private async Task<int> GetCurrentNonce(string accountAddress)
        {
            var currentNonce = await _contractsManager.Web3.Eth.Transactions
                .GetTransactionCount.SendRequestAsync(accountAddress, BlockParameter.CreatePending());

            return (int)currentNonce.Value;
        }

        public async Task<RawTransaction> GetDefaultRawTransaction(string senderAddress)
        {
            var nonce = await GetCurrentNonce(senderAddress);
            return new RawTransaction
            {
                Nonce = nonce,
                AddressTo = _contractsManager.GetGovernorDelegator(),
            };
        }

        public async Task<RawTransaction> GetDefaultTokenTransaction(string senderAddress)
        {
            var nonce = await GetCurrentNonce(senderAddress);
            return new RawTransaction
            {
                Nonce = nonce,
                AddressTo = _contractsManager.GetTokenAddress(),
            };
        }
    }
}
