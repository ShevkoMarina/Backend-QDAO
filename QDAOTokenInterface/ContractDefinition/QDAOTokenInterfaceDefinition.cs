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

namespace BackendQDAO.Contracts.QDAOTokenInterface.ContractDefinition
{


    public partial class QDAOTokenInterfaceDeployment : QDAOTokenInterfaceDeploymentBase
    {
        public QDAOTokenInterfaceDeployment() : base(BYTECODE) { }
        public QDAOTokenInterfaceDeployment(string byteCode) : base(byteCode) { }
    }

    public class QDAOTokenInterfaceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public QDAOTokenInterfaceDeploymentBase() : base(BYTECODE) { }
        public QDAOTokenInterfaceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GetPastVotesFunction : GetPastVotesFunctionBase { }

    [Function("getPastVotes", "uint96")]
    public class GetPastVotesFunctionBase : FunctionMessage
    {
        [Parameter("address", "account", 1)]
        public virtual string Account { get; set; }
        [Parameter("uint256", "blockNumber", 2)]
        public virtual BigInteger BlockNumber { get; set; }
    }

    public partial class TotalSupplyFunction : TotalSupplyFunctionBase { }

    [Function("totalSupply", "uint256")]
    public class TotalSupplyFunctionBase : FunctionMessage
    {

    }

    public partial class GetPastVotesOutputDTO : GetPastVotesOutputDTOBase { }

    [FunctionOutput]
    public class GetPastVotesOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint96", "", 1)]
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
