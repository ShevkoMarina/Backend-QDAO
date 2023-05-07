using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Token;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("token")]
    public class TokenController : ControllerBase
    {
        private IMediator _mediator;

        public TokenController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("balance")]
        public async Task<ActionResult<long>> GetBalance([FromQuery] string signer, CancellationToken ct)
        {
            var query = new GetBalanceQuery.Request(signer);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Balance);
        }

        [HttpGet("current-vote-weight")]
        public async Task<ActionResult> GetCurrentVoteWeight([FromQuery] string address, CancellationToken ct)
        {
            var query = new GetBalanceQuery.Request(address);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Balance);
        }


        [HttpGet("delegate")]
        public async Task<ActionResult> Delegate([FromQuery] string delegatee, CancellationToken ct)
        {
            var signer = "0xd3b8B391c21F9B2DF09a30a3C8B270227a49cC4D";
            var query = new DelegateVotesQuery.Request(signer, delegatee);
            var response = await _mediator.Send(query, ct);

            return Ok(response.TransactionData);
        }

        [HttpGet("transfer")]
        public async Task<ActionResult> Transfer(
            [FromQuery] string dstAccount,
            [FromQuery] uint amount,
            CancellationToken ct)
        {
            var signer = "0xd3b8B391c21F9B2DF09a30a3C8B270227a49cC4D";
            var query = new TransferTokensQuery.Request(signer, dstAccount, amount);
            var response = await _mediator.Send(query, ct);

            return Ok(response.TransactionData);
        }
    }
}
