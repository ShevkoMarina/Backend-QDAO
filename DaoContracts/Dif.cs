using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaoContracts
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Numerics;
    using Nethereum.Hex.HexTypes;
    using Nethereum.ABI.FunctionEncoding.Attributes;
    using Nethereum.Web3;
    using Nethereum.RPC.Eth.DTOs;
    using Nethereum.Contracts.CQS;
    using Nethereum.Contracts;
    using System.Threading;

    namespace SimpleStorage.Contracts.SimpleStorage.ContractDefinition
    {
        //0x81D15cF2045830575f60e90C6Ab8b7b9c52eD09F contract address
        public partial class SimpleStorageDeployment : SimpleStorageDeploymentBase
        {
            public SimpleStorageDeployment() : base(BYTECODE) { }
            public SimpleStorageDeployment(string byteCode) : base(byteCode) { }
        }

        public class SimpleStorageDeploymentBase : ContractDeploymentMessage
        {
            public static string BYTECODE = "0x";
            public SimpleStorageDeploymentBase() : base(BYTECODE) { }
            public SimpleStorageDeploymentBase(string byteCode) : base(byteCode) { }

        }

        public partial class GetFunction : GetFunctionBase { }

        [Function("get", "uint256")]
        public class GetFunctionBase : FunctionMessage
        {

        }

        [Function("set")]
        public class SetNumberTransaction : FunctionMessage
        {
            [Parameter("uint256", "x", 1)]
            public virtual BigInteger X { get; set; }
        }

        public partial class GetOutputDTO : GetOutputDTOBase { }

        [FunctionOutput]
        public class GetOutputDTOBase : IFunctionOutputDTO
        {
            [Parameter("uint256", "", 1)]
            public virtual BigInteger ReturnValue1 { get; set; }
        }
    }
}
