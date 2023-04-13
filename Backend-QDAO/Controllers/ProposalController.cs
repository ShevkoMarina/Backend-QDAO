using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.Services;
using QDAO.Domain;
using QDAO.Endpoint.DTOs;
using QDAO.Endpoint.DTOs.Proposal;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("proposal")]
    public class ProposalController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly TransactionCreator _transactionService;

        public ProposalController(
            TransactionCreator transactionService,
            IMediator mediator)
        {

            _mediator = mediator;
            _transactionService = transactionService;
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> GetProposalsCount(CancellationToken ct)
        {
            var query = new GetProposalsCountQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(((long)response.ProposalsCount));
        }


        [HttpGet("{proposalId::long}")]
        public async Task<ActionResult<Proposal>> GetProposalById(
            [FromRoute] long proposalId,
            CancellationToken ct)
        {
            var query = new GetProposalByIdQuery.Request(proposalId);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Proposal);
        }


        [HttpPost("create")]
        public async Task<ActionResult<RawTransaction>> CreateProposal(
            [FromBody] CreateProposalDto request,
            CancellationToken ct)
        {
            var query = new CreateProposalTxQuery.Request(
                request.Name,
                request.Description,
                ProposalType.UpdateVotingPeriod, 
                request.NewValue);

            var response = await _mediator.Send(query, ct);

            return Ok(response.RawTransaction);
        }


        [HttpPost("queue")]
        public async Task<ActionResult> QueueProposal(
            [FromQuery] uint proposalId,
            [FromQuery] string account,
            CancellationToken ct)
        {
            var query = new QueueProposal.Request(proposalId, account);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }


        [HttpPost("vote")]
        public async Task<ActionResult> VoteForProposal([FromQuery] uint proposalId, [FromQuery] bool support, CancellationToken ct)
        {
            var query = new VoteProposal.Request(proposalId,support);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpPost("approve")]
        public async Task<ActionResult> ApproveProposal([FromQuery] uint proposalId, CancellationToken ct)
        {
            var query = new ApproveByPrincipal.Request(proposalId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        // Нужен синхронизатор статусов пропозалов
        // Тут нужен тонкий пропозал - тока имя и статус
        

        [HttpGet("get-by-user")]
        public async Task<ActionResult<IReadOnlyCollection<ProposalThin>>> GetProposalsByUserId(
            [FromQuery] long userId,
            CancellationToken ct)        {
            // можно мониторить переходы по статусам и алертить если что то пошло не так

            var query = new GetProposalsByUserQuery.Request(userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Proposals);
        }
    }
}
