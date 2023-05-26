using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.Admin;
using QDAO.Application.Handlers.DAO;
using QDAO.Application.Handlers.Proposal;
using QDAO.Application.Handlers.Token;
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
            var query = new AddPrincipalQuery.Request(senderId, userLogin, requiredApprovals);

            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpGet("updatable-settings")]
        public async Task<ActionResult> GetUpdatableSettingsInfo(CancellationToken ct)
        {
            var query = new GetUpdatableSettingsValues.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [HttpGet("transfer-tokens")]
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
    }
}
