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

namespace BackendQDAO.Contracts.GovernorEvents.ContractDefinition
{


    public partial class GovernorEventsDeployment : GovernorEventsDeploymentBase
    {
        public GovernorEventsDeployment() : base(BYTECODE) { }
        public GovernorEventsDeployment(string byteCode) : base(byteCode) { }
    }

    public class GovernorEventsDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "6080604052348015600f57600080fd5b50603f80601d6000396000f3fe6080604052600080fdfea264697066735822122088bfb2793ec888fa6b1a1b624336c0d99d20e0b0d919dcc22896298f5597f7e464736f6c63430008110033";
        public GovernorEventsDeploymentBase() : base(BYTECODE) { }
        public GovernorEventsDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class ProposalCreatedEventDTO : ProposalCreatedEventDTOBase { }

    [Event("ProposalCreated")]
    public class ProposalCreatedEventDTOBase : IEventDTO
    {
        [Parameter("uint256", "id", 1, false )]
        public virtual BigInteger Id { get; set; }
        [Parameter("address", "proposer", 2, false )]
        public virtual string Proposer { get; set; }
        [Parameter("address[]", "targets", 3, false )]
        public virtual List<string> Targets { get; set; }
        [Parameter("uint256[]", "values", 4, false )]
        public virtual List<BigInteger> Values { get; set; }
        [Parameter("bytes[]", "calldatas", 5, false )]
        public virtual List<byte[]> Calldatas { get; set; }
        [Parameter("uint256", "startBlock", 6, false )]
        public virtual BigInteger StartBlock { get; set; }
        [Parameter("uint256", "endBlock", 7, false )]
        public virtual BigInteger EndBlock { get; set; }
        [Parameter("string", "description", 8, false )]
        public virtual string Description { get; set; }
    }
}
