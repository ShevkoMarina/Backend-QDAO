using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.Services;
using QDAO.Endpoint.DTOs;
using System.Numerics;
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
        private readonly TransactionService _transactionService;

        public ProposalController(
            TransactionService transactionService,
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
        public async Task<ActionResult<Transaction>> CreateProposal(
            [FromBody] CreateProposalDto request,
            CancellationToken ct)
        {
            var query = new CreateProposalTxQuery.Request(request.Name, request.Description);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }


        [HttpPost("queue")]
        public async Task QueueProposal()
        {

        }

        [HttpPost("execute")]
        public async Task ExecuteProposal()
        {

        }

        [HttpPost("execute-transaction")]
        public async Task ExecuteTransaction([FromBody] string transaction)
        {
            await _transactionService.Execute(transaction);
        }
    }
}
