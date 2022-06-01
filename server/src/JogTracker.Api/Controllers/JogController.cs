using JogTracker.Api.Core;
using JogTracker.Api.Validation;
using JogTracker.Models.Jogs;
using Microsoft.AspNetCore.Mvc;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/jogs")]
    public class JogController : ControllerBase
    {
        private readonly IJogHandler _jogHandler;
        private readonly IJogValidator _jogValidator;

        public JogController(IJogHandler jogHandler, IJogValidator jogValidator)
        {
            _jogHandler = jogHandler;
            _jogValidator = jogValidator;
        }        

        [HttpPost]
        [Route("")]
        public IActionResult CreateJog([FromBody] JogPayload jogPayload)
        {
            var validationResult = _jogValidator.Validate(jogPayload);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var createdJog = _jogHandler.CreateJog(jogPayload);
            return CreatedAtAction(nameof(GetJog), new { jogId = createdJog.Id }, createdJog);
        }

        [HttpGet]
        [Route("{jogId}")]
        public IActionResult GetJog([FromRoute] int jogId)
        {
            if (! _jogHandler.IsJogExist(jogId))
                return NotFound();

            var jog = _jogHandler.GetJogById(jogId);
            return Ok(jog);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetJogs(
            string searchText = "",
            int pageIndex = 1,
            int pageSize = 20,
            string filter = "",
            bool desc = true)
        {
            return Ok();
        }
    }
}
