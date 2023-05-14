using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Admin;
using QDAO.Application.Handlers.DAO;
using QDAO.Endpoint.DTOs.qDAO;
using System.ComponentModel.DataAnnotations;
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

        [HttpGet("add-principal")]
        public async Task<ActionResult> AddPrincipals(
            [FromQuery] [Required] string userLogin,
            [FromQuery] [Required] short requiredApprovals,
            [FromQuery] [Required] int senderId,
            CancellationToken ct)
        {
            // todo: add validations
            var query = new AddPrincipalsQuery.Request(senderId, userLogin, requiredApprovals);

            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
    }
}
