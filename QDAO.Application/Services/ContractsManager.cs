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

 
        private string _delegator = "0x75954640082E0a700c7BB0334ee501b48A4141FE";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0x0197c028a2037bc6b98BCC4c56539d075CAD5E1f";
        }

        public string GetTokenAddress()
        {
            return "0xbbCc61BB58A4d9C94b013407DbFd7a6d151cdB75";
        }

        public string GetMultisigAddress()
        {
            return "0xaE65233b8B0f448100FdaA45e47204B65d795Dfd";
        }
    }
}
