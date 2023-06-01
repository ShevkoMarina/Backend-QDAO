using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.DAO;
using QDAO.Application.Handlers.Token;
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

        [Authorize(Roles = "admin")]
        [HttpGet("add-principal")]
        public async Task<ActionResult> AddPrincipals(
            [FromQuery] [Required] string userLogin,
            [FromQuery] [Required] short requiredApprovals,
            [FromQuery] [Required] int senderId,
            CancellationToken ct)
        {
            var query = new AddPrincipalQuery.Request(senderId, userLogin, requiredApprovals);

            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("updatable-settings")]
        public async Task<ActionResult> GetUpdatableSettingsInfo(CancellationToken ct)
        {
            var query = new GetUpdatableSettingsValues.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
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

        [Authorize(Roles = "principal")]
        [HttpGet("approve-implementation")]
        public async Task<ActionResult> GetApproveNewImplementationTransaction(
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new ApproveNewImplementationTxQuery.Request(userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("implementation-info")]
        public async Task<ActionResult> GetPendingImplementationInfo(CancellationToken ct)
        {
            var query = new GetPendingImplementationInfoQuery.Request();
            var response = await _mediator.Send(query, ct);

            return Ok(response.PendingAddress);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("set-pending")]
        public async Task<ActionResult> SetPendingImplementationTransaction(
            [FromQuery] int userId,
            [FromQuery] string address,
            CancellationToken ct)
        {
            var query = new SetPendingImplementationTxQuery.Request(userId, address);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("set-implementation")]
        public async Task<ActionResult> SetNewImplementationTransaction(
            [FromQuery] int userId,
            CancellationToken ct)
        {
            var query = new SetNewImplementationTxQuery.Request(userId);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }
    }
}
