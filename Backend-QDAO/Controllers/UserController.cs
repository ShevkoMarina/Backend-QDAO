using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.User;
using QDAO.Endpoint.DTOs;
using QDAO.Endpoint.DTOs.User;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;    
        }

        [HttpPost("auth")]
        public async Task<ActionResult<AuthorizeUserResponseDto>> AuthorizeUser(
            [FromBody] AuthorizeUserRequestDto request, 
            CancellationToken ct)
        {
            var query = new AuthorizeUserQuery.Request(request.Login, request.Password);
            var response = await _mediator.Send(query, ct);

            return Ok(response);
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddUser(
            [FromBody] AddUserDto request, 
            CancellationToken ct)
        {
            var query = new AddUserCommand.Request(request.Login, request.Password, request.Account);
            var response = await _mediator.Send(query, ct);

            return Ok();
        }
    }
}
