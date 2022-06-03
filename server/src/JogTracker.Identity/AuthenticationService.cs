using JogTracker.Common.Exceptions;
using JogTracker.Configuration;
using JogTracker.Models.DTO.Account;
using JogTracker.Models.DTO.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;

namespace JogTracker.Identity
{
    public interface IAuthenticationService
    {
        AuthResult Authenticate(User user);
        string RefreshJwt(string jwt);
        User GetUserFromJwt(string jwt);
    }

    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtSecurityTokenHandler _tokenHandler;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly SymmetricSecurityKey _secret;

        public AuthenticationService(ITokenGenerator tokenGenerator, IConfiguration configuration)
        {
            _tokenHandler = new JwtSecurityTokenHandler();
            _tokenGenerator = tokenGenerator;
            _secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.IdentitySettings.Secret));

        }

        public AuthResult Authenticate(User user)
        {
            return new AuthResult
            {
                CurrentUser = user,
                Jwt = new JwtPair()
                {
                    AccessToken = _tokenGenerator.GenerateAccessToken(user),
                    RefreshToken = _tokenGenerator.GenerateRefreshToken(),
                }
            };
        }

        public string RefreshJwt(string jwt)
        {
            var validatedJwt = ValidateJwt(jwt, false);

            if (validatedJwt == null)
                throw new UnauthorizedException("Invalid access token");

            var user = GetUserFromJwt(validatedJwt);
            return _tokenGenerator.GenerateAccessToken(user);
        }

        public User GetUserFromJwt(string jwt)
        {
            var validatedJwt = ValidateJwt(jwt, false);
            return GetUserFromJwt(validatedJwt);
        }

        private User GetUserFromJwt(JwtSecurityToken jwt)
        {
            try
            {
                return new User
                {
                    Id = jwt.Claims.SingleOrDefault(c => c.Type == "primarysid").Value,
                    UserName = jwt.Claims.SingleOrDefault(c => c.Type == "unique_name").Value,
                    Role = jwt.Claims.SingleOrDefault(c => c.Type == "role").Value
                };
            }
            catch
            {
                return null;
            }
        }

        private JwtSecurityToken ValidateJwt(string jwt, bool validateLifetime)
        {
            try
            {
                _tokenHandler.ValidateToken(jwt, new TokenValidationParameters
                {
                    IssuerSigningKey = _secret,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = validateLifetime,
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
    }
}
