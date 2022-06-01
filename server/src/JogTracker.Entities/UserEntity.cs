using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace JogTracker.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public RefreshTokenEntity RefreshToken { get; set; }
        public IEnumerable<JogEntity> Jogs { get; set; }
    }
}
