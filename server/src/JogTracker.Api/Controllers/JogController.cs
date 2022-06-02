using JogTracker.Api.Core;
using JogTracker.Api.Validation;
using JogTracker.Identity;
using JogTracker.Models.Constants;
using JogTracker.Models.Jogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/jogs")]
    public class JogController : ControllerBase
    {
        private readonly IJogHandler _jogHandler;
        private readonly IJogValidator _jogValidator;
        private readonly IUserContext UserContext;

        public JogController(IJogHandler jogHandler, IJogValidator jogValidator, IUserContext userContext)
        {
            _jogHandler = jogHandler;
            _jogValidator = jogValidator;
            UserContext = userContext;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public IActionResult CreateJog([FromBody] JogPayload jogPayload)
        {
            jogPayload.UserId = UserContext.CurrentUser.Id;

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
            if (!_jogHandler.IsJogExist(jogId))
                return NotFound();

            var jog = _jogHandler.GetJogById(jogId);
            return Ok(jog);
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetJogs(
            string searchText = "",
            DateTime? dateFrom = null,
            DateTime? dateTo = null,
            int pageIndex = 1,
            int pageSize = 20,
            JogSortBy sortBy = JogSortBy.Date,
            bool desc = true)
        {
            var query = new JogQuery(searchText, dateFrom, dateTo, pageSize, pageIndex, sortBy, desc);
            var jogsResponse = _jogHandler.GetJogsByQuery(query);
            
            return Ok(jogsResponse);
        }

        [HttpDelete]
        [Route("{jogId}")]
        [Authorize]
        public IActionResult DeleteJog([FromRoute] int jogId)
        {
            if (!_jogHandler.IsJogExist(jogId))
                return NotFound();

            var jogToDelete = _jogHandler.GetJogById(jogId);

            if (!HasAccessToJog(jogToDelete))
                return Forbid();

            _jogHandler.DeleteJog(jogId);

            return NoContent();
        }

        private bool HasAccessToJog(Jog jog)
        {
            return jog.User.Id == UserContext.CurrentUser.Id || UserContext.CurrentUser.Role != Roles.Administrator;
        }
    }
}
