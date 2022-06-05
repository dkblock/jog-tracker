using JogTracker.Models.DTO.Account;
using JogTracker.Models.Requests.Users;
using MediatR;

namespace JogTracker.Models.Requests.Account
{
    public class RegisterCommand : UserNamePayload, IRequest<AuthResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
