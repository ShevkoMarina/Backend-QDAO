using Nethereum.Web3;

namespace QDAO.Application.Services
{
    public class ContractsManager
    {
        public Web3 Web3 { get; set; }

        public ContractsManager()
        {
            // web3 = new Nethereum.Web3.Web3("https://eth-goerli.g.alchemy.com/v2/PU1jr72jAHmucb_oUHObuiwoCCsdtODL");
            Web3 = new Nethereum.Web3.Web3("http://192.168.1.45:8545");
        }

 
        private string _delegator = "0x808f7bb1Fb2f9387211a1CD3bdf7aBF85a04c92c";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0xc81C8aD68DB1D7450926197F0035B8A093f94B7A";
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
