using JogTracker.Database;
using JogTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Repository
{
    public interface IJogsRepository : IRepositoryBase<JogEntity>
    {
        Task<JogEntity> GetWithChildren(string id);
    }

    public class JogsRepository : RepositoryBase<JogEntity>, IJogsRepository
    {
        private readonly ApplicationContext _context;

        public JogsRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
        
        public async Task<JogEntity> GetWithChildren(string id)
        {
            return await _context.Jogs
                .AsNoTracking()
                .Where(j => j.Id == id)
                .Include(j => j.User)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }
}
