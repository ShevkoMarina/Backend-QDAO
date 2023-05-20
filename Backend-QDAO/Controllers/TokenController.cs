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
            [FromQuery] int userId,
            [FromQuery] string delefateeLogin,
            [FromQuery] long amount,
            CancellationToken ct)
        {
            var query = new TransferTokensQuery.Request(userId, delefateeLogin, amount);
            var response = await _mediator.Send(query, ct);

            return Ok(response.TransactionData);
        }

        [HttpGet("total")]
        public async Task<ActionResult> GetTotalSupply(CancellationToken ct)
        {
            var query = new GetTotalSupplyQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
    }
}
