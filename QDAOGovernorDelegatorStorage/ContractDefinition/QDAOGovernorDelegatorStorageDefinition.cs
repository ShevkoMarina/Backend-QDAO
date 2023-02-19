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

namespace BackendQDAO.Contracts.QDAOGovernorDelegatorStorage.ContractDefinition
{


    public partial class QDAOGovernorDelegatorStorageDeployment : QDAOGovernorDelegatorStorageDeploymentBase
    {
        public QDAOGovernorDelegatorStorageDeployment() : base(BYTECODE) { }
        public QDAOGovernorDelegatorStorageDeployment(string byteCode) : base(byteCode) { }
    }

    public class QDAOGovernorDelegatorStorageDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b5060c98061001f6000396000f3fe6080604052348015600f57600080fd5b5060043610603c5760003560e01c8063267822471460415780635c60da1b14606f578063f851a440146081575b600080fd5b6001546053906001600160a01b031681565b6040516001600160a01b03909116815260200160405180910390f35b6002546053906001600160a01b031681565b6000546053906001600160a01b03168156fea2646970667358221220d3d8b6d46c099c3dbf5e6dce5192f637b4008d5dca45be94d5a92ac23b86916b64736f6c63430008110033";
        public QDAOGovernorDelegatorStorageDeploymentBase() : base(BYTECODE) { }
        public QDAOGovernorDelegatorStorageDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class AdminFunction : AdminFunctionBase { }

    [Function("admin", "address")]
    public class AdminFunctionBase : FunctionMessage
    {

    }

    public partial class ImplementationFunction : ImplementationFunctionBase { }

    [Function("implementation", "address")]
    public class ImplementationFunctionBase : FunctionMessage
    {

    }

    public partial class PendingAdminFunction : PendingAdminFunctionBase { }

    [Function("pendingAdmin", "address")]
    public class PendingAdminFunctionBase : FunctionMessage
    {

    }

    public partial class AdminOutputDTO : AdminOutputDTOBase { }

    [FunctionOutput]
    public class AdminOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class ImplementationOutputDTO : ImplementationOutputDTOBase { }

    [FunctionOutput]
    public class ImplementationOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class PendingAdminOutputDTO : PendingAdminOutputDTOBase { }

    [FunctionOutput]
    public class PendingAdminOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }
}
