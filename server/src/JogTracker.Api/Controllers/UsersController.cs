using JogTracker.Common.Constants;
using JogTracker.Common.Extensions;
using JogTracker.Models.Requests.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JogTracker.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = Roles.Administrator)]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetUsers(
            [FromQuery] string searchText = "",
            [FromQuery] string role = Roles.Any,
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 20,
            [FromQuery] UsersSortBy sortBy = UsersSortBy.LastName,
            [FromQuery] bool desc = false)
        {
            var query = new GetUsersQuery(searchText, role, pageSize, pageIndex, sortBy, desc);
            var usersPage = await _mediator.Send(query);

            return Ok(usersPage);
        }

        [HttpDelete]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string userId)
        {
            var payload = new DeleteUserCommand(userId, User.GetUserId());
            await _mediator.Send(payload);

            return NoContent();
        }

        [HttpPut]
        [Route("{userId}")]
        public async Task<IActionResult> DeleteUser([FromBody] UpdateUserCommand payload)
        {
            payload.UserId = User.GetUserId();
            var updatedUser = await _mediator.Send(payload);

            return Ok(updatedUser);
        }
    }
}
