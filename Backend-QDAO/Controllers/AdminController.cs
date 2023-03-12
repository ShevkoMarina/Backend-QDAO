using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator)
        {

            _mediator = mediator;
        }

        [HttpGet("initialize-governor")]
        public async Task<ActionResult> InitializeGovernor(
            CancellationToken ct)
        {
            var query = new InitializeGovernorCommand.Request();
            var response = await _mediator.Send(query, ct);

            return Ok();
        }
        
    }
}
