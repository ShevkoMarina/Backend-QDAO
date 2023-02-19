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

namespace BackendQDAO.Contracts.QDAOGovernorDelegateStorageV1.ContractDefinition
{


    public partial class QDAOGovernorDelegateStorageV1Deployment : QDAOGovernorDelegateStorageV1DeploymentBase
    {
        public QDAOGovernorDelegateStorageV1Deployment() : base(BYTECODE) { }
        public QDAOGovernorDelegateStorageV1Deployment(string byteCode) : base(byteCode) { }
    }

    public class QDAOGovernorDelegateStorageV1DeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "608060405234801561001057600080fd5b50610243806100206000396000f3fe608060405234801561001057600080fd5b50600436106100935760003560e01c8063a7713a7011610066578063a7713a70146101a9578063d33219b4146101b2578063da35c664146101c5578063f851a440146101ce578063fc0c546a146101e157600080fd5b8063013cf08b1461009857806302a251a314610154578063267822471461016b5780635c60da1b14610196575b600080fd5b6100ff6100a63660046101f4565b600760208190526000918252604090912080546001820154600283015460068401549484015460088501546009860154600a9096015494966001600160a01b039094169592949192909160ff8082169161010090041689565b60408051998a526001600160a01b0390981660208a0152968801959095526060870193909352608086019190915260a085015260c0840152151560e08301521515610100820152610120015b60405180910390f35b61015d60035481565b60405190815260200161014b565b60015461017e906001600160a01b031681565b6040516001600160a01b03909116815260200161014b565b60025461017e906001600160a01b031681565b61015d60085481565b60055461017e906001600160a01b031681565b61015d60045481565b60005461017e906001600160a01b031681565b60065461017e906001600160a01b031681565b60006020828403121561020657600080fd5b503591905056fea26469706673582212209ce42629c8b71bcd16622913c9bfb7210184e1aae4f8dbbaa406399e99fa637064736f6c63430008110033";
        public QDAOGovernorDelegateStorageV1DeploymentBase() : base(BYTECODE) { }
        public QDAOGovernorDelegateStorageV1DeploymentBase(string byteCode) : base(byteCode) { }

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

    public partial class ProposalCountFunction : ProposalCountFunctionBase { }

    [Function("proposalCount", "uint256")]
    public class ProposalCountFunctionBase : FunctionMessage
    {

    }

    public partial class ProposalsFunction : ProposalsFunctionBase { }

    [Function("proposals", typeof(ProposalsOutputDTO))]
    public class ProposalsFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class QuorumNumeratorFunction : QuorumNumeratorFunctionBase { }

    [Function("quorumNumerator", "uint256")]
    public class QuorumNumeratorFunctionBase : FunctionMessage
    {

    }

    public partial class TimelockFunction : TimelockFunctionBase { }

    [Function("timelock", "address")]
    public class TimelockFunctionBase : FunctionMessage
    {

    }

    public partial class TokenFunction : TokenFunctionBase { }

    [Function("token", "address")]
    public class TokenFunctionBase : FunctionMessage
    {

    }

    public partial class VotingPeriodFunction : VotingPeriodFunctionBase { }

    [Function("votingPeriod", "uint256")]
    public class VotingPeriodFunctionBase : FunctionMessage
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

    public partial class ProposalCountOutputDTO : ProposalCountOutputDTOBase { }

    [FunctionOutput]
    public class ProposalCountOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class ProposalsOutputDTO : ProposalsOutputDTOBase { }

    [FunctionOutput]
    public class ProposalsOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "id", 1)]
        public virtual BigInteger Id { get; set; }
        [Parameter("address", "proposer", 2)]
        public virtual string Proposer { get; set; }
        [Parameter("uint256", "eta", 3)]
        public virtual BigInteger Eta { get; set; }
        [Parameter("uint256", "startBlock", 4)]
        public virtual BigInteger StartBlock { get; set; }
        [Parameter("uint256", "endBlock", 5)]
        public virtual BigInteger EndBlock { get; set; }
        [Parameter("uint256", "forVotes", 6)]
        public virtual BigInteger ForVotes { get; set; }
        [Parameter("uint256", "againstVotes", 7)]
        public virtual BigInteger AgainstVotes { get; set; }
        [Parameter("bool", "canceled", 8)]
        public virtual bool Canceled { get; set; }
        [Parameter("bool", "executed", 9)]
        public virtual bool Executed { get; set; }
    }

    public partial class QuorumNumeratorOutputDTO : QuorumNumeratorOutputDTOBase { }

    [FunctionOutput]
    public class QuorumNumeratorOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }

    public partial class TimelockOutputDTO : TimelockOutputDTOBase { }

    [FunctionOutput]
    public class TimelockOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class TokenOutputDTO : TokenOutputDTOBase { }

    [FunctionOutput]
    public class TokenOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class VotingPeriodOutputDTO : VotingPeriodOutputDTOBase { }

    [FunctionOutput]
    public class VotingPeriodOutputDTOBase : IFunctionOutputDTO 
    {
        [Parameter("uint256", "", 1)]
        public virtual BigInteger ReturnValue1 { get; set; }
    }
}
