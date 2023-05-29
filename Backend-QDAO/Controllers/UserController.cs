using MediatR;
using Microsoft.AspNetCore.Mvc;
using QDAO.Application.Handlers.User;
using QDAO.Endpoint.DTOs;
using QDAO.Endpoint.DTOs.Error;
using QDAO.Endpoint.DTOs.User;
using System;
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
            try
            {
                var query = new AuthorizeUserQuery.Request(request.Login, request.Password);
                var response = await _mediator.Send(query, ct);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return ex.ToHttp();
            }
        }


        [HttpPost("add")]
        public async Task<ActionResult> AddUser(
            [FromBody] AddUserDto request, 
            CancellationToken ct)
        {
            try
            {
                var query = new AddUserCommand.Request(request.Login, request.Password, request.Account);
                var response = await _mediator.Send(query, ct);

                return Ok();
            }
            catch (Exception ex)
            {
                return ex.ToHttp();
            }
        }

        [HttpPost("init-admin")]
        public async Task<ActionResult> InitAdmin(
            [FromBody] AddUserDto request,
            CancellationToken ct)
        {
            try
            {
                var query = new AddAdminCommand.Request(request.Login, request.Password, request.Account);
                var response = await _mediator.Send(query, ct);

                return Ok();
            }
            catch (Exception ex)
            {
                return ex.ToHttp();
            }
        }
    }
}
