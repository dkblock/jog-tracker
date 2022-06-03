using MediatR;

namespace JogTracker.Models.Commands.Account
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
