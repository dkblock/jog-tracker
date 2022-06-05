using JogTracker.Common.Exceptions;
using JogTracker.Entities;
using JogTracker.Identity;
using JogTracker.Models.DTO.Account;
using JogTracker.Models.Requests.Authentication;
using JogTracker.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Authentication
{
    public class RefreshJwtCommandHandler : IRequestHandler<RefreshJwtCommand, JwtPair>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public RefreshJwtCommandHandler(IAuthenticationService authenticationService, IRefreshTokensRepository refreshTokensRepository)
        {
            _authenticationService = authenticationService;
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<JwtPair> Handle(RefreshJwtCommand payload, CancellationToken cancellationToken)
        {
            var accessToken = payload.Jwt.AccessToken;
            var refreshToken = payload.Jwt.RefreshToken;

            var user = _authenticationService.GetUserFromJwt(accessToken);
            var refreshTokenEntity = await _refreshTokensRepository.GetByUserId(user.Id);

            if (!IsRefreshTokenValid(refreshToken, refreshTokenEntity))
                throw new UnauthorizedException("Invalid refresh token");

            var newAccessToken = _authenticationService.RefreshJwt(payload.Jwt.AccessToken);

            return new JwtPair
            {
                AccessToken = newAccessToken,
                RefreshToken = refreshToken
            };
        }

        private bool IsRefreshTokenValid(string refreshToken, RefreshTokenEntity entity) =>
            entity != null && refreshToken == entity.Value && DateTime.Now <= entity.ExpirationDate;
    }
}
