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
        public Web3 Web3 { get; set; }

        public ContractsManager()
        {
            // web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
            Web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");
        }

        // временно
        private string _governorAddress = "0x25B9a573399CF9D1E50fcdE89aB8782271531CeE";

        public string GetGovernorAddress()
        {
            return _governorAddress;
        }

        public string GetTimelockAddress()
        {
            return "0x4e6Ea8622c1fc417d38BBE22D36Ed10848266843";
        }

        public string GetTokenAddress()
        {
            return "0x21c50fE0A33AA7F45d714AD4CEd5f8904B0a53fA";
        }

        public Web3 GetWeb3FromAdminAccount()
        {
            var account = new ManagedAccount("0x75c09fb19051f8F13B0C8BdD7e7c3BE123821C77", "01d25758cdfb1eeae4c79abda2491a3d9e5f003c5527815d0052a1910450386b");
            return new Web3(account, "https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
        }
    }
}
