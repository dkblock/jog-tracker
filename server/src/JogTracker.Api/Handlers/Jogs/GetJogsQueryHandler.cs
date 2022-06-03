using AutoMapper;
using JogTracker.Common.Helpers;
using JogTracker.Entities;
using JogTracker.Models.DTO;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.Queries.Jogs;
using JogTracker.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Jogs
{
    public class GetJogsQueryHandler : IRequestHandler<GetJogsQuery, PageResponse<Jog>>
    {
        private readonly IJogsRepository _jogsRepository;
        private readonly IMapper _mapper;

        public GetJogsQueryHandler(IJogsRepository jogsRepository, IMapper mapper)
        {
            _jogsRepository = jogsRepository;
            _mapper = mapper;
        }

        public Task<PageResponse<Jog>> Handle(GetJogsQuery request, CancellationToken cancellationToken)
        {
            var jogs = _jogsRepository
                .GetQueryable()
                .Include(j => j.User)
                .AsQueryable();

            if (request.OnlyOwn && !string.IsNullOrEmpty(request.UserId))
                jogs = jogs.Where(j => j.UserId == request.UserId);

            if (request.DateFrom.HasValue)
                jogs = jogs.Where(j => j.Date >= request.DateFrom);

            if (request.DateTo.HasValue)
                jogs = jogs.Where(j => j.Date <= request.DateTo);

            if (!string.IsNullOrEmpty(request.SearchText))
                jogs = jogs.Where(j => 
                    QueryHelper.IsMatch(request.SearchText, j.User.UserName) ||
                    QueryHelper.IsMatch(request.SearchText, j.User.FirstName) ||
                    QueryHelper.IsMatch(request.SearchText, j.User.LastName));

            var sortedJogs = request.SortByDesc
                ? jogs.OrderByDescending(JogsSortModel[request.SortBy])
                : jogs.OrderBy(JogsSortModel[request.SortBy]);

            var result = sortedJogs
                .Skip(request.PageSize * (request.PageIndex - 1))
                .Take(request.PageSize);

            return Task.FromResult(new PageResponse<Jog>
            {
                Page = _mapper.Map<IEnumerable<Jog>>(result),
                TotalCount = sortedJogs.Count(),
            });
        }

        private IDictionary<JogSortBy, Func<JogEntity, object>> JogsSortModel =>
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
