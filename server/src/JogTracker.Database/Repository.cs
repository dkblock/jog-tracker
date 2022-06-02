using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JogTracker.Database
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);
        bool Exists(object id);
        TEntity Get(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetByQuery(DbQuery<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties);
        void Delete(object id);
        void Update(object id, TEntity entity);
    }

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationContext _context;
        private readonly DbSet<TEntity> _entities;

        public Repository(ApplicationContext context)
        {
            _context = context;
            _entities = _context.Set<TEntity>();
        }

        public TEntity Create(TEntity entity)
        {
            var result = _entities.Add(entity);
            _context.SaveChanges();

            return result.Entity;
        }

        public bool Exists(object id)
        {
            return Get(id) != null;
        }

        public TEntity Get(object id)
        {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public IEnumerable<TEntity> GetWithInclude(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            return Include(includeProperties);
        }

        public IEnumerable<TEntity> GetByQuery(DbQuery<TEntity> query, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var entities = Include(includeProperties).Where(query.SearchPredicate);
            var sortedEntities = query.SortByDesc
                ? entities.OrderByDescending(query.SortBy)
                : entities.OrderBy(query.SortBy);

            return sortedEntities.Skip(query.PageSize * (query.PageIndex - 1));
        }

        public void Delete(object id)
        {
            var entity = Get(id);
            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(object id, TEntity entity)
        {
            var entityToUpdate = Get(id);

            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        private IQueryable<TEntity> Include(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            var query = _entities.AsNoTracking();
            return includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
