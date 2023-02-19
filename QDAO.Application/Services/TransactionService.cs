﻿using Nethereum.RPC.Eth.DTOs;
using QDAO.Domain;
using System.Threading.Tasks;

namespace QDAO.Application.Services
{
    public class TransactionService
    {
        private const string Web3Url = "https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL";

        private Nethereum.Web3.Web3 _web3Client;

        public TransactionService()
        {
            _web3Client = new Nethereum.Web3.Web3(Web3Url);
        }

        public async Task<string> Execute(string transaction)
        {
            var txHash = await _web3Client.Eth.Transactions.SendRawTransaction.SendRequestAsync(transaction);

            return txHash;
        }

        public async Task<int> GetCurrentNonce(string accountAddress)
        {
            var currentNonce = await _web3Client.Eth.Transactions.GetTransactionCount.SendRequestAsync(accountAddress, BlockParameter.CreatePending());

            return (int)currentNonce.Value;
        }

        public async Task<RawTransaction> GetDefaultRawTransaction(string senderAddress)
        {
        
            var nonce = await GetCurrentNonce(senderAddress);
            return new RawTransaction
            {
                Nonce = nonce,
                AddressTo = "0x2980343ce6E94aA17c5499139AB3532D98095321", // governor
                Gas = 20000000000,
                GasLimit = 200000,
                Value = 0
            };
        }
    }
}
