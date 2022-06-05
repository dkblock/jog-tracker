using JogTracker.Models.DTO.Account;
using MediatR;

namespace JogTracker.Models.Requests.Authentication
{
    public class RefreshJwtCommand : IRequest<JwtPair>
    {
        public JwtPair Jwt { get; set; }
    }
}
