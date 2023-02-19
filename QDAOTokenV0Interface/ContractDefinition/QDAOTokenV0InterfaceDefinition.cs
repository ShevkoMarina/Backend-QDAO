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

namespace BackendQDAO.Contracts.QDAOTokenV0Interface.ContractDefinition
{


    public partial class QDAOTokenV0InterfaceDeployment : QDAOTokenV0InterfaceDeploymentBase
    {
        public QDAOTokenV0InterfaceDeployment() : base(BYTECODE) { }
        public QDAOTokenV0InterfaceDeployment(string byteCode) : base(byteCode) { }
    }

    public class QDAOTokenV0InterfaceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public QDAOTokenV0InterfaceDeploymentBase() : base(BYTECODE) { }
        public QDAOTokenV0InterfaceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetCurrentVotesFunction : GetCurrentVotesFunctionBase { }

    [Function("getCurrentVotes", "uint256")]
    public class GetCurrentVotesFunctionBase : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class GetCurrentVotesOutputDTO : GetCurrentVotesOutputDTOBase { }

    [FunctionOutput]
    public class GetCurrentVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TotalSupplyOutputDTO : TotalSupplyOutputDTOBase { }

    [FunctionOutput]
    public class TotalSupplyOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
