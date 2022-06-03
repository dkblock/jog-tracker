using JogTracker.Models.DTO.Account;
using MediatR;

namespace JogTracker.Models.Commands.Authentication
{
    public class RefreshJwtCommand : IRequest<JwtPair>
    {
        public JwtPair Jwt { get; set; }
    }
}
