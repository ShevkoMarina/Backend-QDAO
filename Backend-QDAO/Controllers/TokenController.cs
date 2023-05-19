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


        [HttpGet("user-info")]
        public async Task<ActionResult<long>> GetUserTokenInfo(
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new GetBalanceQuery.Request(userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }


        [HttpGet("delegate")]
        public async Task<ActionResult> Delegate(
            [FromQuery] int userId,
            [FromQuery] string delegateeLogin,
            CancellationToken ct)
        {
            var query = new DelegateVotesQuery.Request(userId, delegateeLogin);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
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
