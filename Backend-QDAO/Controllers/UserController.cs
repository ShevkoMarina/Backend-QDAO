using Microsoft.AspNetCore.Mvc;
using QDAO.Endpoint.DTOs;
using QDAO.Endpoint.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace QDAO.Endpoint.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        [Route("auth")]
        [HttpPost]
        public async Task<ActionResult> AuthorizeUser([FromBody] AuthorizeUserRequestDto request, CancellationToken ct)
        {
            return Ok();
        }


        [Route("add")]
        [HttpPost]
        public async Task<ActionResult> AddUser([FromBody] AddUserDto request, CancellationToken ct)
        {
            return Ok();
        }
    }
}
