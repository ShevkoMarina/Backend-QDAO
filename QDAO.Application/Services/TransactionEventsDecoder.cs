using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using QDAO.Application.Services.DTOs.Events;
using System.Collections.Generic;
using System.Linq;

namespace QDAO.Application.Services
{
    public class TransactionEventsDecoder
    {
        public IReadOnlyCollection<IEventDTO> Decode(TransactionReceipt receipt)
        {
            var events = receipt.DecodeAllEvents<ProposalCreatedEventDto>();

            return events.Select(e => (IEventDTO)e.Event).ToList();
        }
    }
}
