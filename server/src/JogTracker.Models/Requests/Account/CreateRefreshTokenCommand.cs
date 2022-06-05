using MediatR;

namespace JogTracker.Models.Requests.Account
{
    public class CreateRefreshTokenCommand : IRequest
    {
        public CreateRefreshTokenCommand(string value, string userId)
        {
            Value = value;
            UserId = userId;
        }

        public string Value { get; set; }
        public string UserId { get; set; }
    }
}
