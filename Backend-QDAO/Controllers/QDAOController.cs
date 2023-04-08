using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Admin;
using QDAO.Application.Handlers.DAO;
using QDAO.Endpoint.DTOs.qDAO;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("qdao")]
    public class QDAOController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QDAOController(IMediator mediator)
        {

            _mediator = mediator;
        }


        [HttpPost("create-qdao")]
        public async Task<ActionResult> CreateQDAO(
            [FromBody] CreateQDAODto request,
            CancellationToken ct)
        {

            var query = new CreateDAOQuery.Request(request.AdminAddress, request.TotalSupply, request.Delay);
            var response = await _mediator.Send(query, ct);

            return Ok(response.Result);
        }

        [HttpGet("update-governor")]
        public async Task<ActionResult> UpdateGovernorImplementation(CancellationToken ct)
        {
            return Ok();
        }

        [HttpGet("accept")]
        public async Task<ActionResult> AcceptNewImplementation(CancellationToken ct)
        {
            return Ok();
        }
    }
}
