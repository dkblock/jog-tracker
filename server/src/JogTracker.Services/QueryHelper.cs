using JogTracker.Entities;
using JogTracker.Models.Jogs;
using System;
using System.Collections.Generic;

namespace JogTracker.Services
{
    public interface IQueryHelper
    {
        bool IsMatch(string searchText, string[] filters);

        IDictionary<JogSortBy, Func<JogEntity, object>> JogSortModel { get; }
    }

    public class QueryHelper : IQueryHelper
    {
        public bool IsMatch(string searchText, string[] filters)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;

            foreach (var filter in filters)
            {
                if (filter.ToLower().Contains(searchText.ToLower()))
                    return true;
            }

            return false;
        }

        public IDictionary<JogSortBy, Func<JogEntity, object>> JogSortModel =>
            new Dictionary<JogSortBy, Func<JogEntity, object>>
            {
                { JogSortBy.Name, x => $"{x.User.LastName} {x.User.FirstName}" },
                { JogSortBy.Username, x => $"{x.User.LastName} {x.User.FirstName}" },
                { JogSortBy.Date, x => x.Date },
                { JogSortBy.Distance, x => x.DistanceInMeters },
                { JogSortBy.ElapsedTime, x => x.ElapsedTimeInSeconds },
                { JogSortBy.AverageSpeed, x => x.AverageSpeedInMetersPerSecond },
            };
    }
}
