using JogTracker.Configuration;
using JogTracker.Entities;
using JogTracker.Identity;
using JogTracker.Mappers;
using JogTracker.Models.Account;
using JogTracker.Models.Settings;
using JogTracker.Models.Users;
using JogTracker.Services;
using System;
using System.Threading.Tasks;

namespace JogTracker.Api.Core
{
    public interface IAccountHandler
    {
        Task<AuthResponse> Register(RegisterPayload registerPayload);
        Task<AuthResponse> Login(LoginPayload loginPayload);
        void Test(string accessToken);
    }

    public class AccountHandler : IAccountHandler
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IUserService _userService;
        private readonly IUserMapper _userMapper;
        private readonly IdentitySettings _settings;

        public AccountHandler(
            IAuthenticationService authenticationService,
            IRefreshTokenService refreshTokenService,
            IUserService userService,
            IUserMapper userMapper,
            IConfiguration configuration)

        {
            _authenticationService = authenticationService;
            _refreshTokenService = refreshTokenService;
            _userService = userService;
            _userMapper = userMapper;
            _settings = configuration.IdentitySettings;
        }

        public void Test(string accessToken)
        {
            _authenticationService.ValidateToken(accessToken);
        }

        public async Task<AuthResponse> Register(RegisterPayload registerPayload)
        {
            var userEntity = _userMapper.ToEntity(registerPayload);
            await _userService.CreateUser(userEntity, registerPayload.Password);

            var user = await GetUserWithRole(userEntity);
            var authResponse = _authenticationService.Authenticate(user);

            CreateRefreshToken(userEntity, authResponse.JWT.RefreshToken);

            return authResponse;
        }

        public async Task<AuthResponse> Login(LoginPayload loginPayload)
        {
            var userEntity = await _userService.GetByUsername(loginPayload.Username);
            var user = await GetUserWithRole(userEntity);
            var authResponse = _authenticationService.Authenticate(user);

            DeleteRefreshToken(userEntity);
            CreateRefreshToken(userEntity, authResponse.JWT.RefreshToken);

            return authResponse;
        }

        private void CreateRefreshToken(UserEntity user, string refreshToken)
        {
            var refreshTokenEntity = new RefreshTokenEntity
            {
                Value = refreshToken,
                ExpirationDate = DateTime.Now.AddDays(_settings.RefreshTokenLifetimeInDays),
                UserId = user.Id,
            };

            _refreshTokenService.CreateRefreshToken(refreshTokenEntity);
        }

        private void DeleteRefreshToken(UserEntity user)
        {
            if (_refreshTokenService.IsRefreshTokenExistByUserId(user.Id))
                _refreshTokenService.DeleteRefreshTokenByUserId(user.Id);
        }

        private async Task<User> GetUserWithRole(UserEntity userEntity)
        {
            var user = _userMapper.ToModel(userEntity);
            user.Role = await _userService.GetUserRole(user.Id);

            return user;
        }
    }
}
