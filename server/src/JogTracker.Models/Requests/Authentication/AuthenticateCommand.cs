using JogTracker.Models.DTO.Users;
using MediatR;

namespace JogTracker.Models.Requests.Authentication
{
    public class AuthenticateCommand : IRequest<User>
    {
        public AuthenticateCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
