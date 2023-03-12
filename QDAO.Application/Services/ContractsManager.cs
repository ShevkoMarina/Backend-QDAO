using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QDAO.Application.Services
{
    public class ContractsManager
    {
        private Web3 web3;

        public ContractsManager()
        {
            web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
        }

        // временно
        private string _governorAddress = "0xebdf7907577Dbd00a0E55479e24B17AecA8384ed";

        public string GetGovernorAddress()
        {
            return _governorAddress;
        }

        public string GetTimelockAddress()
        {
            return "0xB289545bBF4443b03CC44F8BaF65E86DAF9d90A9";
        }

        public string GetTokenAddress()
        {
            return "0xc78EB1c2d7b19C087B5d00Ea9D980D4746e7Bc39";
        }

        public Web3 GetWeb3()
        {
            return web3;
        }

        public Web3 GetWeb3FromAdminAccount()
        {
            var account = new ManagedAccount("0x75c09fb19051f8F13B0C8BdD7e7c3BE123821C77", "01d25758cdfb1eeae4c79abda2491a3d9e5f003c5527815d0052a1910450386b");
            return new Web3(account, "https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
        }
    }
}
