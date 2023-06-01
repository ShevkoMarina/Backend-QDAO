using Nethereum.Web3;

namespace QDAO.Application.Services
{
    public class ContractsManager
    {
        public Web3 Web3 { get; set; }

        public ContractsManager()
        {
            // web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
            // Web3 = new Nethereum.Web3.Web3("http://192.168.1.45:8545");
            Web3 = new Nethereum.Web3.Web3("http://127.0.0.1:8545");
        }

 
        private string _delegator = "0x6eEb9A45FDfC556Ed30caA56231643a4Be0C2fb6";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0x951CF9D9933322D30d1e9bF91A6b31916E80BfFF";
        }

        public string GetTokenAddress()
        {
            return "0x77319Ba2966e1985cB90f5eAc8A3C7CC7723824C";
        }

        public string GetMultisigAddress()
        {
            return "0xb2Ecc407D2Fd1d1314CF5dCfdD8Bce0221af37Eb";
        }
    }
}
