using Nethereum.Web3;

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

 
        private string _delegator = "0x45D18E0D7172d550505b61B6C86919C3c6e6c129";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0x4Bdd7222e70b2a9D8396A082BfE41970Ff37De4D";
        }

        public string GetTokenAddress()
        {
            return "0x0fef7931488416C57974E232D749bC36dda3C258";
        }
    }
}
