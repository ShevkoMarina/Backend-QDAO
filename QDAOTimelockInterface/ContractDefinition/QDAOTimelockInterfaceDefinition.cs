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

namespace BackendQDAO.Contracts.QDAOTimelockInterface.ContractDefinition
{


    public partial class QDAOTimelockInterfaceDeployment : QDAOTimelockInterfaceDeploymentBase
    {
        public QDAOTimelockInterfaceDeployment() : base(BYTECODE) { }
        public QDAOTimelockInterfaceDeployment(string byteCode) : base(byteCode) { }
    }

    public class QDAOTimelockInterfaceDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "";
        public QDAOTimelockInterfaceDeploymentBase() : base(BYTECODE) { }
        public QDAOTimelockInterfaceDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class GRACE_PERIODFunction : GRACE_PERIODFunctionBase { }

    [Function("GRACE_PERIOD", "uint256")]
    public class GRACE_PERIODFunctionBase : FunctionMessage
    {

    }

    public partial class AcceptAdminFunction : AcceptAdminFunctionBase { }

    [Function("acceptAdmin")]
    public class AcceptAdminFunctionBase : FunctionMessage
    {

    }

    public partial class CancelTransactionFunction : CancelTransactionFunctionBase { }

    [Function("cancelTransaction")]
    public class CancelTransactionFunctionBase : FunctionMessage
    {
        [Parameter("address", "target", 1)]
        public virtual string Target { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 3)]
        public virtual byte[] Data { get; set; }
        [Parameter("uint256", "eta", 4)]
        public virtual BigInteger Eta { get; set; }
    }

    public partial class DelayFunction : DelayFunctionBase { }

    [Function("delay", "uint256")]
    public class DelayFunctionBase : FunctionMessage
    {

    }

    public partial class ExecuteTransactionFunction : ExecuteTransactionFunctionBase { }

    [Function("executeTransaction", "bytes")]
    public class ExecuteTransactionFunctionBase : FunctionMessage
    {
        [Parameter("address", "target", 1)]
        public virtual string Target { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 3)]
        public virtual byte[] Data { get; set; }
        [Parameter("uint256", "eta", 4)]
        public virtual BigInteger Eta { get; set; }
    }

    public partial class QueueTransactionFunction : QueueTransactionFunctionBase { }

    [Function("queueTransaction", "bytes32")]
    public class QueueTransactionFunctionBase : FunctionMessage
    {
        [Parameter("address", "target", 1)]
        public virtual string Target { get; set; }
        [Parameter("uint256", "value", 2)]
        public virtual BigInteger Value { get; set; }
        [Parameter("bytes", "data", 3)]
        public virtual byte[] Data { get; set; }
        [Parameter("uint256", "eta", 4)]
        public virtual BigInteger Eta { get; set; }
    }

    public partial class QueuedTransactionsFunction : QueuedTransactionsFunctionBase { }

    [Function("queuedTransactions", "bool")]
    public class QueuedTransactionsFunctionBase : FunctionMessage
    {
        [Parameter("bytes32", "hash", 1)]
        public virtual byte[] Hash { get; set; }
    }

    public partial class GRACE_PERIODOutputDTO : GRACE_PERIODOutputDTOBase { }

    [FunctionOutput]
    public class GRACE_PERIODOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class DelayOutputDTO : DelayOutputDTOBase { }

    [FunctionOutput]
    public class DelayOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }





    public partial class QueuedTransactionsOutputDTO : QueuedTransactionsOutputDTOBase { }

    [FunctionOutput]
    public class QueuedTransactionsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
