using JogTracker.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Repository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task Create(T entity);
        Task<T> Get(string id);
        IQueryable<T> GetQueryable();
        Task<bool> Exists(string id);
        Task Delete(T entity);
        Task Update(T entity);
        Task SaveChanges();
    }

    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<T> _entities;

        public RepositoryBase(ApplicationContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }

        public async Task Create(T entity)
        {
            await _entities.AddAsync(entity);
            await SaveChanges();
        }

        public async Task<T> Get(string id)
        {
            return await _entities.FindAsync(id);
        }

        public IQueryable<T> GetQueryable()
        {
            return _entities.AsQueryable().AsNoTracking();
        }

        public async Task<bool> Exists(string id)
        {
            return await Get(id) != null;
        }

        public async Task Delete(T entity)
        {
            await Task.FromResult(_entities.Remove(entity));
            await SaveChanges();
        }

        public async Task Update(T entity)
        {
            await Task.FromResult(_entities.Update(entity));
            await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
