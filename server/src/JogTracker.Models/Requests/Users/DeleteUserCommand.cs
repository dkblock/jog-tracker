using MediatR;

namespace JogTracker.Models.Requests.Users
{
    public class DeleteUserCommand : IRequest
    {
        public DeleteUserCommand(string id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
    }
}
