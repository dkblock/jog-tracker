using AutoMapper;
using JogTracker.Common.Constants;
using JogTracker.Common.Helpers;
using JogTracker.Entities;
using JogTracker.Models.DTO;
using JogTracker.Models.DTO.Jogs;
using JogTracker.Models.Requests.Jogs;
using JogTracker.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

            var page = _mapper.Map<IEnumerable<Jog>>(result);

            if (await _usersRepository.IsExistById(query.UserId))
            {
                var currentUser = await _usersRepository.GetById(query.UserId);
                var isAdmin = !string.IsNullOrEmpty(query.UserId) && currentUser.Role == Roles.Administrator;

                foreach (var jog in page)
                {
                    jog.HasAccess = jog.User.Id == query.UserId || isAdmin;
                }
            }           

            return new PageResponse<Jog>
            {
                Page = page,
                TotalCount = sortedJogs.Count(),
            };
        }

        private IDictionary<JogsSortBy, Expression<Func<JogEntity, object>>> JogsSortModel =>
            new Dictionary<JogsSortBy, Expression<Func<JogEntity, object>>>
            {
                { JogsSortBy.Name, x => x.User.LastName },
                { JogsSortBy.Username, x => x.User.UserName },
                { JogsSortBy.Date, x => x.Date },
                { JogsSortBy.Distance, x => x.DistanceInMeters },
                { JogsSortBy.ElapsedTime, x => x.ElapsedTimeInSeconds },
                { JogsSortBy.AverageSpeed, x => x.AverageSpeedInMetersPerSecond },
            };
    }
}
