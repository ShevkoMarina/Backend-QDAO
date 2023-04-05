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

 
        private string _delegator = "0x25B9a573399CF9D1E50fcdE89aB8782271531CeE";

        public string GetGovernorDelegator()
        {
            return _delegator;
        }

        public string GetTimelockAddress()
        {
            return "0x4e6Ea8622c1fc417d38BBE22D36Ed10848266843";
        }

        public string GetTokenAddress()
        {
            return "0x21c50fE0A33AA7F45d714AD4CEd5f8904B0a53fA";
        }
    }
}
