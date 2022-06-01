using JogTracker.Database;
using JogTracker.Entities;
using System.Linq;

namespace JogTracker.Services
{
    public interface IRefreshTokenService
    {
        RefreshTokenEntity CreateRefreshToken(RefreshTokenEntity refreshToken);
        bool IsRefreshTokenExistByUserId(string userId);
        void DeleteRefreshTokenByUserId(string userId);
    }

    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRepository<RefreshTokenEntity> _repository;

        public RefreshTokenService(IRepository<RefreshTokenEntity> repository)
        {
            _repository = repository;
        }

        public RefreshTokenEntity CreateRefreshToken(RefreshTokenEntity refreshToken)
        {
            return _repository.Create(refreshToken);
        }

        public bool IsRefreshTokenExistByUserId(string userId)
        {
            return _repository.Get(t => t.UserId == userId).SingleOrDefault() != null;
        }

        public void DeleteRefreshTokenByUserId(string userId)
        {
            var refreshToken = _repository.Get(t => t.UserId == userId).Single();
            _repository.Delete(refreshToken.Id);
        }        
    }
}
