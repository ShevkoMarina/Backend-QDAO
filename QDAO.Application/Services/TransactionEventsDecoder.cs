using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;
using QDAO.Application.Pipelines.Events;
using QDAO.Application.Services.DTOs.Events;
using System.Collections.Generic;
using System.Linq;

namespace QDAO.Application.Services
{
    public class TransactionEventsDecoder
    {
        public IReadOnlyCollection<IEventDTO> Decode(TransactionReceipt receipt)
        {
            
            var creationEvents = receipt.DecodeAllEvents<ProposalCreatedEventDto>();
            if (creationEvents.Any())
            {
                return creationEvents.Select(e => (IEventDTO)e.Event).ToList();
            }

            var queuedEvents = receipt.DecodeAllEvents<ProposalQueuedEventDto>();
            if (queuedEvents.Any())
            {
                return queuedEvents.Select(e => (IEventDTO)e.Event).ToList();
            }

            var voteCastedEvents = receipt.DecodeAllEvents<VoteCastedEventDto>();
            if (voteCastedEvents.Any())
            {
                return voteCastedEvents.Select(e => (IEventDTO)e.Event).ToList();
            }

            var principalAdded = receipt.DecodeAllEvents<PrincipalAddedEventDto>();
            if (principalAdded.Any())
            {
                return principalAdded.Select(e => (IEventDTO)e.Event).ToList();
            }

            var proposalApprovedEvent = receipt.DecodeAllEvents<ProposalApprovedEventDto>();
            if (proposalApprovedEvent.Any())
            {
                return proposalApprovedEvent.Select(e => (IEventDTO)e.Event).ToList();
            }

            var proposalExecutedEvent = receipt.DecodeAllEvents<ProposalExecutedEventDto>();
            if (proposalExecutedEvent.Any())
            {
                return proposalExecutedEvent.Select(e => (IEventDTO)e.Event).ToList();
            }


            return new List<IEventDTO>();
        }
    }
}
