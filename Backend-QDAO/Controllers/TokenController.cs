using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Token;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [Route("balance")]
        [HttpGet]
        public async Task<ActionResult> GetBalance([FromQuery] string address, CancellationToken ct)
        {
            var query = new GetBalanceQuery.Request(address);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Balance);
        }
    }
}
