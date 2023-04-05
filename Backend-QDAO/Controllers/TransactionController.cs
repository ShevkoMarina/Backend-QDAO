using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Transaction;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{

    [ApiController]
    [Route("transaction")]
    public class TransactionController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("execute")]
        public async Task<ActionResult<string>> ExecuteTransaction(
            [FromBody] string transaction,
            CancellationToken ct)
        {
            var command = new ExecuteTransactionCommand.Request(transaction);
            var response = await _mediator.Send(command, ct);

            return Ok(response.TxHash);
        }

        
        [HttpGet("events")]
        public async Task<ActionResult> GetProposalEventsByTxHash(
            [FromQuery] string hash,
            CancellationToken ct)
        {
            var query = new GetEventsByTransactionHashQuery.Request(hash);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Events);
        }
        
    }
}
