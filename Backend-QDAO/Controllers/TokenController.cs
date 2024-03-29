﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("user-info")]
        public async Task<ActionResult<long>> GetUserTokenInfo(
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new GetUserTokenInfo.Request(userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet("total")]
        public async Task<ActionResult> GetTotalSupply(CancellationToken ct)
        {
            var query = new GetTotalSupplyQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
    }
}
