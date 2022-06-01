using JogTracker.Api.Core;
using JogTracker.Api.Validation;
using JogTracker.Models.Account;
using JogTracker.Models.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountHandler _accountHandler;
        private readonly IAccountValidator _accountValidator;

        public AccountController(IAccountHandler accountHandler, IAccountValidator accountValidator)
        {
            _accountHandler = accountHandler;
            _accountValidator = accountValidator;
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterPayload registerPayload)
        {
            var validationResult = await _accountValidator.ValidateOnRegister(registerPayload);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var authResponse = await _accountHandler.Register(registerPayload);
            return Ok(authResponse);
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginPayload loginPayload)
        {
            var validationResult = await _accountValidator.ValidateOnLogin(loginPayload);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var authResponse = await _accountHandler.Login(loginPayload);
            return Ok(authResponse);
        }

        [HttpPost]
        [Route("test")]
        [Authorize]
        public IActionResult Test([FromBody] LoginPayload loginPayload)
        {
            _accountHandler.Test(loginPayload.Password);
            return Ok();
        }
    }
}
