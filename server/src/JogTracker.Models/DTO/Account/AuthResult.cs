using JogTracker.Models.DTO.Users;

namespace JogTracker.Models.DTO.Account
{
    public class AuthResult
    {
        public User CurrentUser { get; set; }
        public JwtPair Jwt { get; set; }
    }
}
