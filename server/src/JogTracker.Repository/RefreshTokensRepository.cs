using JogTracker.Database;
using JogTracker.Entities;

namespace JogTracker.Repository
{
    public interface IRefreshTokensRepository : IRepositoryBase<RefreshTokenEntity>
    {

    }

    public class RefreshTokensRepository : RepositoryBase<RefreshTokenEntity>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
