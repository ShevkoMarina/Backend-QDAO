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


        [HttpPost("create")]
        public async Task<ActionResult<RawTransaction>> CreateProposal(
            [FromBody] CreateProposalDto request,
            CancellationToken ct)
        {
            var query = new CreateProposalTxQuery.Request(
                request.Name,
                request.Description,
                ProposalType.UpdateVotingPeriod, 
                request.UserId,
                request.NewValue);
            
            var response = await _mediator.Send(query, ct);

            return Ok(response.RawTransaction);
        }


        [HttpPost("queue")]
        public async Task<ActionResult> QueueProposal(
            [FromQuery] long proposalId,
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new QueueProposal.Request(proposalId, userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpPost("execute")]
        public async Task<ActionResult> ExecuteProposal(
            [FromQuery] long proposalId,
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new GetProposalExecutionTransactionQuery.Request(proposalId, userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }


        [HttpGet("vote")]
        public async Task<ActionResult> VoteForProposal(
            [FromQuery] int userId,
            [FromQuery] long proposalId, 
            [FromQuery] bool support,
            CancellationToken ct)
        {
            var query = new VoteProposal.Request(proposalId,support, userId);
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

        [HttpGet("info")]
        public async Task<ActionResult> GetProposalInfoById(
            [FromQuery] long proposalId,
            CancellationToken ct)
        {

            var query = new GetProposalInfoById.Request(proposalId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpGet("active")]
        public async Task<ActionResult> GetProposalsActiveForVoting(CancellationToken ct)
        {
            var query = new GetProposalsActiveForVotingQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpGet("promotion")]
        public async Task<ActionResult> GetProposalsForPromotion(CancellationToken ct)
        {
            var query = new GetProposalsForPromotionQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
    }
}
