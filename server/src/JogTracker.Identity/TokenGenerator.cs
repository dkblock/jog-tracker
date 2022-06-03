using JogTracker.Configuration;
using JogTracker.Models.DTO.Users;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JogTracker.Identity
{
    public interface ITokenGenerator
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }

    public class TokenGenerator : ITokenGenerator
    {
        private readonly SymmetricSecurityKey _secret;
        private readonly string _signingAlgorithm;
        private readonly int _accessTokenLifetime;

        public TokenGenerator(IConfiguration configuration)
        {
            _secret = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.IdentitySettings.Secret));
            _signingAlgorithm = SecurityAlgorithms.HmacSha256Signature;
            _accessTokenLifetime = configuration.IdentitySettings.AccessTokenLifetimeInMinutes;
        }

        public string GenerateAccessToken(User user)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                 {
                    new Claim(ClaimTypes.PrimarySid, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, user.Role)
                 }),
                Expires = DateTime.Now.AddMinutes(_accessTokenLifetime),
                SigningCredentials = new SigningCredentials(_secret, _signingAlgorithm)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            RandomNumberGenerator.Create().GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
