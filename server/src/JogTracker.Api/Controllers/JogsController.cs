using JogTracker.Common.Extensions;
using JogTracker.Models.Commands.Jogs;
using JogTracker.Models.Queries.Jogs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/jogs")]
    public class JogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        [Authorize]
        public async Task<IActionResult> CreateJog([FromBody] CreateJogCommand payload)
        {
            payload.UserId = User.GetUserId();
            var createdJog = await _mediator.Send(payload);

            return CreatedAtAction(nameof(GetJog), new { jogId = createdJog.Id }, createdJog);
        }

        [HttpGet]
        [Route("{jogId}")]
        public IActionResult GetJog([FromRoute] int jogId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetJogs(
            [FromQuery] string searchText = "",
            [FromQuery] DateTime? dateFrom = null,
            [FromQuery] DateTime? dateTo = null,
            [FromQuery] bool onlyOwn = false,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] JogSortBy sortBy = JogSortBy.Date,
            [FromQuery] bool desc = true)
        {
            var query = new GetJogsQuery(searchText, dateFrom, dateTo, onlyOwn, pageSize, pageIndex, sortBy, desc, User.GetUserId());
            var jogsPage = await _mediator.Send(query);
            
            return Ok(jogsPage);
        }

        //[HttpDelete]
        //[Route("{jogId}")]
        //[Authorize]
        //public IActionResult DeleteJog([FromRoute] int jogId)
        //{
        //    if (!_jogHandler.IsJogExist(jogId))
        //        return NotFound();

        //    var jogToDelete = _jogHandler.GetJogById(jogId);

        //    if (!HasAccessToJog(jogToDelete))
        //        return Forbid();

        //    _jogHandler.DeleteJog(jogId);

        //    return NoContent();
        //}

        //private bool HasAccessToJog(Jog jog)
        //{
        //    return jog.User.Id == UserContext.CurrentUser.Id || UserContext.CurrentUser.Role != Roles.Administrator;
        //}
    }
}
