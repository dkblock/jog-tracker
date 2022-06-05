using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> Authenticate()
        {
            var payload = new AuthenticateCommand(User.GetUserId());
            var user = await _mediator.Send(payload);

            return Ok(user);
        }

        [HttpPost]
        [Route("refresh")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshJwt([FromBody] RefreshJwtCommand payload)
        {
            var jwt = await _mediator.Send(payload);
            return Ok(jwt);
        }
    }
}
