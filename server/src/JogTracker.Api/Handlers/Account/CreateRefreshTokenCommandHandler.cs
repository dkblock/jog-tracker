using AutoMapper;
using JogTracker.Configuration;
using JogTracker.Entities;
using JogTracker.Models.Requests.Account;
using JogTracker.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Account
{
    public class CreateRefreshTokenCommandHandler : IRequestHandler<CreateRefreshTokenCommand>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CreateRefreshTokenCommandHandler(
            IRefreshTokensRepository refreshTokensRepository,
            IConfiguration configuration,
            IMapper mapper)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(CreateRefreshTokenCommand payload, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RefreshTokenEntity>(payload);
            entity.ExpirationDate = DateTime.Now.AddDays(_configuration.IdentitySettings.RefreshTokenLifetimeInDays);

            var refreshToken = await _refreshTokensRepository.GetByUserId(payload.UserId);

            if (refreshToken != null)
            {
                refreshToken.Value = entity.Value;
                refreshToken.ExpirationDate = entity.ExpirationDate;
                await _refreshTokensRepository.Update(refreshToken);
            }
            else
            {
                entity.Id = Guid.NewGuid().ToString();
                await _refreshTokensRepository.Create(entity);
            }            

            return Unit.Value;
        }
    }
}
