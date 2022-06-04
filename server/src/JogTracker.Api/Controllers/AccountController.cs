using JogTracker.Common.Extensions;
using JogTracker.Models.Commands.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterCommand payload)
        {
            var authResponse = await _mediator.Send(payload);
            return Ok(authResponse);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginCommand payload)
        {
            var authResponse = await _mediator.Send(payload);
            return Ok(authResponse);
        }

        [HttpGet]
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Login()
        {
            var payload = new LogoutCommand(User.GetUserId());
            await _mediator.Send(payload);

            return Ok();
        }
    }
}
