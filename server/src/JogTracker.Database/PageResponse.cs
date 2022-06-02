using System.Collections.Generic;

namespace JogTracker.Entities
{
    public class PageResponse<TEntity> where TEntity : class
    {
        public IEnumerable<TEntity> Page { get; set; }
        public int TotalCount { get; set; }
    }
}
