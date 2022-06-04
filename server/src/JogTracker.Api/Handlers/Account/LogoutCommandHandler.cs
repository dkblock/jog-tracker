using JogTracker.Models.Commands.Account;
using JogTracker.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Account
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public LogoutCommandHandler(IRefreshTokensRepository refreshTokensRepository)
        {
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<Unit> Handle(LogoutCommand payload, CancellationToken cancellationToken)
        {
            var token = await _refreshTokensRepository.GetByUserId(payload.UserId);
            await _refreshTokensRepository.Delete(token);

            return Unit.Value;
        }
    }
}
