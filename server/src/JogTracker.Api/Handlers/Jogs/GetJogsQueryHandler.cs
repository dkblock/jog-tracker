using AutoMapper;
using JogTracker.Common.Constants;
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
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetJogsQueryHandler(IJogsRepository jogsRepository, IUsersRepository usersRepository, IMapper mapper)
        {
            _jogsRepository = jogsRepository;
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<PageResponse<Jog>> Handle(GetJogsQuery query, CancellationToken cancellationToken)
        {
            var jogs = _jogsRepository
                .GetQueryable()
                .Include(j => j.User)
                .AsQueryable();

            if (query.OnlyOwn && !string.IsNullOrEmpty(query.UserId))
                jogs = jogs.Where(j => j.UserId == query.UserId);

            if (query.DateFrom.HasValue)
                jogs = jogs.Where(j => j.Date.Date >= query.DateFrom.Value.Date);

            if (query.DateTo.HasValue)
                jogs = jogs.Where(j => j.Date.Date <= query.DateTo.Value.Date);

            if (!string.IsNullOrEmpty(query.SearchText))
                jogs = jogs.Where(j =>
                    j.User.FirstName.ToLower().Contains(query.SearchText) ||
                    j.User.LastName.ToLower().Contains(query.SearchText) ||
                    j.User.UserName.ToLower().Contains(query.SearchText));

            var sortedJogs = query.SortByDesc
                ? jogs.OrderByDescending(JogsSortModel[query.SortBy])
                : jogs.OrderBy(JogsSortModel[query.SortBy]);

            var result = sortedJogs
                .Skip(query.PageSize * (query.PageIndex - 1))
                .Take(query.PageSize);

            var page = _mapper.Map<IList<Jog>>(result);
            var isAdmin = !string.IsNullOrEmpty(query.UserId) && await _usersRepository.GetRole(query.UserId) == Roles.Administrator;

            foreach (var jog in page)
            {
                jog.HasAccess = jog.User.Id == query.UserId || isAdmin;
            }

            return new PageResponse<Jog>
            {
                Page = page,
                TotalCount = sortedJogs.Count(),
            };
        }

        private IDictionary<JogSortBy, Func<JogEntity, object>> JogsSortModel =>
            new Dictionary<JogSortBy, Func<JogEntity, object>>
            {
                { JogSortBy.Name, x => $"{x.User.LastName} {x.User.FirstName}" },
                { JogSortBy.Username, x => x.User.UserName },
                { JogSortBy.Date, x => x.Date },
                { JogSortBy.Distance, x => x.DistanceInMeters },
                { JogSortBy.ElapsedTime, x => x.ElapsedTimeInSeconds },
                { JogSortBy.AverageSpeed, x => x.AverageSpeedInMetersPerSecond },
            };
    }
}
