using Nethereum.Contracts;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace TESTING
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");

     //       var result = await web3.Eth.GetBalance.SendRequestAsync("0x9360825F924d0eB3F04F846f44f6938f94e2f907");
            /*
           
            var web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
            var contractAddress = "0x4EcACffE6d5b141DECDe91Ac62c747a8C64ea579";
            web3.TransactionManager.UseLegacyAsDefault = true;

            var address_1 = "0x618E0fFEe21406f493D22f9163c48E2D036de6B0";

            var set = new AddTransaction
            {
                A = 1,
                B = 2
            };
            var setData = set.GetCallData();

            var handler = web3.Eth.GetContractTransactionHandler<AddTransaction>();

            var estimate = await handler.EstimateGasAsync(contractAddress, set);
            Console.WriteLine(estimate);

            var hash = await web3.Eth.Transactions.SendRawTransaction
                  .SendRequestAsync("0xf8aa1a8504a817c800830493e0944ecacffe6d5b141decde91ac62c747a8c64ea57980b844a5f3c23b000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000021ba0b5f690588ce63e78bd9b0ee25d52e2bc9a90b6b4d8e954dce205c0c6f7f79448a077859dca67cde88fd58dfcfcfc1d4141917620c20c5caf6d219644a05cd03235");
             Console.WriteLine(hash);

            //0xab3ba6cde9a8db014f66e71d5810f2342b6a62b123db2c971b2a57af4a1a2528
            /*
           // Web3.OfflineTransactionSigner.GetSenderAddress(encoded).Dump();

            var hex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(setData);

            Console.WriteLine("0x" + hex);

            

            var result = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(hash);

           
            var receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash);
            while (receipt == null)
            {
                Thread.Sleep(1000);
                receipt = await web3.Eth.Transactions.GetTransactionReceipt.SendRequestAsync(hash);
            }

            Console.WriteLine(receipt);
            //var hash = await web3.Eth.Transactions.SendRawTransaction
            //    .SendRequestAsync("0xf8ac1c8504a817c80083030d4096ffff4ecacffe6d5b141decde91ac62c747a8c64ea57980b844a5f3c23b000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000021ca013fa303c435617f92500af2779246cdb5e932c9f836792c236592546fef83a0ea04a9f9829d439fee26c98188bc80d72fbc9f58ec75e8baa8b9188126fd0a241cf");
            */
            
            var connectionString = "User Id=postgres;Password=sukaBlyat123;Server=db.kidrgducdfhhwdjdvwoq.supabase.co;Port=5432;Database=postgres";
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();

                NpgsqlCommand cmd = new NpgsqlCommand("SELECT current_database()", conn);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string columnValue = reader.GetString(0);

                    }
                }

                conn.Close();
            }
            
        }
    }
}
