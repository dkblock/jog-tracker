using MediatR;

namespace JogTracker.Models.Requests.Account
{
    public class LogoutCommand : IRequest
    {
        public LogoutCommand(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; set; }
    }
}
