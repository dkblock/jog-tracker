using JogTracker.Database;
using JogTracker.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Repository
{
    public interface IRefreshTokensRepository : IRepositoryBase<RefreshTokenEntity>
    {
        Task<RefreshTokenEntity> GetByUserId(string userId);
    }

    public class RefreshTokensRepository : RepositoryBase<RefreshTokenEntity>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(ApplicationContext context) : base(context)
        {
        }

        public Task<RefreshTokenEntity> GetByUserId(string userId)
        {
            return Task.FromResult(
                GetQueryable()
                .AsQueryable()
                .Where(t => t.UserId == userId)
                .SingleOrDefault());
        }
    }
}
