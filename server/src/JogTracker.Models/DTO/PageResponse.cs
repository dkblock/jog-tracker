using System.Collections.Generic;

namespace JogTracker.Models.DTO
{
    public class PageResponse<T> where T : class
    {
        public IEnumerable<T> Page { get; set; }
        public int TotalCount { get; set; }
    }
}
