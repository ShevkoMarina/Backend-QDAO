using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
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


          //  var address_1 = "0x618E0fFEe21406f493D22f9163c48E2D036de6B0";

            var set = new AddTransaction
            {
                A = 1,
                B = 2
            };
            var setData = set.GetCallData();
            var hex = Nethereum.Hex.HexConvertors.Extensions.HexByteConvertorExtensions.ToHex(setData);

            //var func = contract.GetFunction("add");

            // Здесь должно быть зашифровано название функции и параметры
            //var data = func.GetData(1, 2);

            //  var decodeData = Nethereum.Hex.HexConvertors.Extensions.HexStringUTF8ConvertorExtensions


            //var hash = await web3.Eth.Transactions.SendRawTransaction
            //    .SendRequestAsync("0xf8aa168504a817c80083030d40944ecacffe6d5b141decde91ac62c747a8c64ea57980b844a5f3c23b000000000000000000000000000000000000000000000000000000000000000100000000000000000000000000000000000000000000000000000000000000021ca0ecbe2a39fe98949f962caf557d0ab7827db337ae73a7eeb774e4375bd0e7c5bea07fdf7a65d66b39f51e76a92c09cc2f5d7cd36236bc5f77392c5b427d9e5df340");
            //Console.WriteLine(hash);

            Console.WriteLine("0x" + hex);
            // Console.WriteLine(data);


            var handler = web3.Eth.GetContractQueryHandler<AddTransaction>();
            await handler.QueryAsync(handler);


        }
    }
}
