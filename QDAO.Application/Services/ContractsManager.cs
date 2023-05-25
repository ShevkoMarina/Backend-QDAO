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

 
        private string _delegator = "0x283e148550026d0262800eD7Bd7d193E841fa1Da";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0xE7929311DDD6e02734519ea3Beb31b900693E37F";
        }

        public string GetTokenAddress()
        {
            return "0xa42C9413eeaD0A194174A99C0d2c554914708289";
        }

        public string GetMultisigAddress()
        {
            return "0xA6fE4cE54D2ac02618060842ccA41aAFd2a0Cf95";
        }
    }
}
