using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3.Accounts;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace TESTING
{
    class Program
    {
        static async Task Main(string[] args)
        {
            
            Console.WriteLine("Hello World!");

            
            var web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
            var contractAddress = "0x4EcACffE6d5b141DECDe91Ac62c747a8C64ea579";
            /*

            var address_1 = "0x618E0fFEe21406f493D22f9163c48E2D036de6B0";

            var set = new AddTransaction
            {
                A = 1,
                B = 2
            };
            var setData = set.GetCallData();

            var hex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(setData);

            Console.WriteLine("0x" + hex);
            
 
            var hash = await web3.Eth.Transactions.SendRawTransaction
                .SendRequestAsync("0xf8ac1c8504a817c80083030d4096ffff4ecacffe6d5b141decde91ac62c747a8c64ea57980b844a5f3c23b000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000021ca013fa303c435617f92500af2779246cdb5e932c9f836792c236592546fef83a0ea04a9f9829d439fee26c98188bc80d72fbc9f58ec75e8baa8b9188126fd0a241cf");
            */

            var address_1 = "0x618E0fFEe21406f493D22f9163c48E2D036de6B0";

            var addFunction = new AddTransaction
            {
                A = 1,
                B = 2,
                Nonce = 27,
                AmountToSend = 0,
                Gas = 20000000000,
                GasPrice = 200000,
            };

            var addHandler = web3.Eth.GetContractTransactionHandler<AddTransaction>();

            var res = await addHandler.SendRequestAndWaitForReceiptAsync(contractAddress, addFunction);

        }
    }
}
