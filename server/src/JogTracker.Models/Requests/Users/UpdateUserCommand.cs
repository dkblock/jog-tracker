using JogTracker.Models.DTO.Users;
using MediatR;

namespace JogTracker.Models.Requests.Users
{
    public class UpdateUserCommand : UserNamePayload, IRequest<User>
    {
        public string Id { get; set; }
        public string Role { get; set; }
        public string UserId { get; set; }
    }
}
