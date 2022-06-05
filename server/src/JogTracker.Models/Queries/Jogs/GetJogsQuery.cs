﻿using JogTracker.Models.DTO;
using JogTracker.Models.DTO.Jogs;
using MediatR;
using System;

namespace JogTracker.Models.Queries.Jogs
{
    public class GetJogsQuery : IRequest<PageResponse<Jog>>
    {
        public GetJogsQuery(
            string searchText,
            DateTime? dateFrom,
            DateTime? dateTo,
            bool onlyOwn,
            int pageSize,
            int pageIndex,
            JogSortBy sortBy,
            bool sortByDesc,
            string userId)
        {
            SearchText = searchText?.ToLower();
            DateFrom = dateFrom;
            DateTo = dateTo;
            OnlyOwn = onlyOwn;
            PageSize = pageSize;
            PageIndex = pageIndex;
            SortBy = sortBy;
            SortByDesc = sortByDesc;
            UserId = userId;
        }

        public string SearchText { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public bool OnlyOwn { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public JogSortBy SortBy { get; set; }
        public bool SortByDesc { get; set; }
        public string UserId { get; set; }
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
