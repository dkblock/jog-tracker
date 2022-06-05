using MediatR;

namespace JogTracker.Models.Requests.Jogs
{
    public class DeleteJogCommand : IRequest
    {
        public DeleteJogCommand(string id, string userId)
        {
            Id = id;
            UserId = userId;
        }

        public string Id { get; set; }
        public string UserId { get; set; }
    }
}
