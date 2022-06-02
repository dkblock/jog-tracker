using System;
using System.Linq.Expressions;

namespace JogTracker.Database
{
    public class DbQuery<TEntity> where TEntity : class
    {
        public DbQuery(
            Expression<Func<TEntity, bool>> searchPredicate,
            Func<TEntity, object> sortBy,
            bool sortByDesc,
            int pageSize,
            int pageIndex)
        {
            SearchPredicate = searchPredicate;
            SortBy = sortBy;
            SortByDesc = sortByDesc;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public Expression<Func<TEntity, bool>> SearchPredicate { get; set; }
        public Func<TEntity, object> SortBy { get; set; }
        public bool SortByDesc { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
