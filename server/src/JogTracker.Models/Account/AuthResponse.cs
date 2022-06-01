using JogTracker.Models.Users;

namespace JogTracker.Models.Account
{
    public class AuthResponse
    {
        public User CurrentUser { get; set; }
        public JwtPair JWT { get; set; }
    }
}
