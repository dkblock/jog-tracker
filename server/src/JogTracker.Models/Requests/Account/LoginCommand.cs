using JogTracker.Models.DTO.Account;
using MediatR;

namespace JogTracker.Models.Requests.Account
{
    public class LoginCommand : IRequest<AuthResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
