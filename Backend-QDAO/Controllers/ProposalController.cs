using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Proposal;
using QDAO.Domain;
using QDAO.Endpoint.DTOs.Error;
using QDAO.Endpoint.DTOs.Proposal;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("proposal")]
    public class ProposalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProposalController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<RawTransaction>> GetCreateProposalTransaction(
            [FromBody] CreateProposalDto request,
            CancellationToken ct)
        {
            try
            {
                var query = new GetCreateProposalTxQuery.Request(
                request.Name,
                request.Description,
                ProposalType.UpdateVotingPeriod,
                request.UserId,
                request.NewValue);

                var response = await _mediator.Send(query, ct);

                return Ok(response.RawTransaction);
            }
            catch (Exception ex) 
            {
                return ex.ToHttp();
            }
        }


        [HttpPost("queue")]
        public async Task<ActionResult> GetQueueProposalTransaction(
            [FromQuery] long proposalId,
            [FromQuery] int userId,
            CancellationToken ct)
        {
            try
            {
                var query = new QueueProposal.Request(proposalId, userId);
                var response = await _mediator.Send(query, ct);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return ex.ToHttp();
            }
        }

        [HttpPost("execute")]
        public async Task<ActionResult> GetExecuteProposalTransaction(
            [FromQuery] long proposalId,
            [FromQuery] int userId,
            CancellationToken ct)
        {
            try
            {
                var query = new GetProposalExecutionTransactionQuery.Request(proposalId, userId);
                var response = await _mediator.Send(query, ct);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return ex.ToHttp();
            }
        }


        [HttpGet("vote")]
        public async Task<ActionResult> GetVotingTransaction(
            [FromQuery] int userId,
            [FromQuery] long proposalId, 
            [FromQuery] bool support,
            CancellationToken ct)
        {
            var query = new VoteProposal.Request(proposalId,support, userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpGet("approve")]
        public async Task<ActionResult> GetApproveProposalTransaction(
            [FromQuery] long proposalId,
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new ApproveByPrincipal.Request(proposalId, userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
        

        [HttpGet("get-by-user")]
        public async Task<ActionResult<IReadOnlyCollection<ProposalThin>>> GetProposalsByUserId(
            [FromQuery] int userId,
            CancellationToken ct)        {

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
