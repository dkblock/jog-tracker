using System;

namespace JogTracker.Models.Jogs
{
    public class JogQuery
    {
        public JogQuery(
            string searchText,
            DateTime? dateFrom,
            DateTime? dateTo,
            int pageSize,
            int pageIndex,
            JogSortBy sortBy,
            bool sortByDesc)
        {
            SearchText = searchText;
            DateFrom = dateFrom;
            DateTo = dateTo;
            PageSize = pageSize;
            PageIndex = pageIndex;
            SortBy = sortBy;
            SortByDesc = sortByDesc;
        }

        public string SearchText { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public JogSortBy SortBy { get; set; }
        public bool SortByDesc { get; set; }
    }

    public enum JogSortBy
    {
        Name,
        Username,
        Date,
        Distance,
        ElapsedTime,
        AverageSpeed,
    }
}
