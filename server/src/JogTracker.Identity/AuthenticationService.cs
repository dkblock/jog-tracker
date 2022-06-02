using JogTracker.Configuration;
using JogTracker.Models.Account;
using JogTracker.Models.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JogTracker.Identity
{
    public interface IAuthenticationService
    {
        AuthResponse Authenticate(User user);
        User GetUserFromPrincipal(ClaimsPrincipal principal);
        JwtSecurityToken ValidateToken(string token);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly SymmetricSecurityKey _secret;
        private readonly string _signingAlgorithm;
        private readonly int _accessTokenLifetime;

        public AuthenticationService(IConfiguration configuration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.IdentitySettings.Secret));
            _signingAlgorithm = SecurityAlgorithms.HmacSha256Signature;
            _accessTokenLifetime = configuration.IdentitySettings.AccessTokenLifetimeInMinutes;
        }

        public AuthResponse Authenticate(User user)
        {
            return new AuthResponse
            {
                CurrentUser = user,
                JWT = new JwtPair()
                {
                    AccessToken = GenerateAccessToken(user),
                    RefreshToken = GenerateRefreshToken(),
                }
            };
        }

        public User GetUserFromPrincipal(ClaimsPrincipal principal)
        {
            try
            {
                return new User
                {
                    Id = principal.FindFirstValue(ClaimTypes.PrimarySid),
                    Username = principal.FindFirstValue(ClaimTypes.Name),
                    Role = principal.FindFirstValue(ClaimTypes.Role),
                };
            } 
            catch
            {
                return new User();
            }
        }

        public string RefreshAccessToken(JwtPair jwt)
        {
            return null;
        }

        public JwtSecurityToken ValidateToken(string token)
        {
            try
            {
                _tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    IssuerSigningKey = _secret,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;
            }
            catch
            {
                return null;
            }
        }

        private string GenerateAccessToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.PrimarySid, user.Id),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.Now.AddMinutes(_accessTokenLifetime),
                SigningCredentials = new SigningCredentials(_secret, _signingAlgorithm)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Create().GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
