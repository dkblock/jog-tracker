using JogTracker.Models.DTO.Account;
using MediatR;

namespace JogTracker.Models.Commands.Account
{
    public class RegisterCommand : IRequest<AuthResult>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
