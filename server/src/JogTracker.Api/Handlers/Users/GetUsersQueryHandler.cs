using AutoMapper;
using JogTracker.Common.Constants;
using JogTracker.Entities;
using JogTracker.Models.DTO;
using JogTracker.Models.DTO.Users;
using JogTracker.Models.Requests.Users;
using JogTracker.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace JogTracker.Api.Handlers.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PageResponse<User>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public GetUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public Task<PageResponse<User>> Handle(GetUsersQuery query, CancellationToken cancellationToken)
        {
            var users = _usersRepository
                .GetQueryable()
                .Include(u => u.Jogs)
                .AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchText))
                users = users.Where(u =>
                    u.FirstName.ToLower().Contains(query.SearchText) ||
                    u.LastName.ToLower().Contains(query.SearchText) ||
                    u.UserName.ToLower().Contains(query.SearchText));

            if (query.Role != Roles.Any)
                users = users.Where(u => u.Role == query.Role);

            var sortedUsers = query.SortByDesc
                ? users.OrderByDescending(UsersSortModel[query.SortBy])
                : users.OrderBy(UsersSortModel[query.SortBy]);

            var result = sortedUsers
                .Skip(query.PageSize * (query.PageIndex - 1))
                .Take(query.PageSize);

            return Task.FromResult(new PageResponse<User>
            {
                Page = _mapper.Map<IEnumerable<User>>(result),
                TotalCount = sortedUsers.Count(),
            });
        }

        private IDictionary<UsersSortBy, Expression<Func<UserEntity, object>>> UsersSortModel =>
            new Dictionary<UsersSortBy, Expression<Func<UserEntity, object>>>
            {
                { UsersSortBy.FirstName, x => x.FirstName },
                { UsersSortBy.LastName, x => x.LastName },
                { UsersSortBy.Username, x => x.UserName },
                { UsersSortBy.Role, x => x.Role },
                { UsersSortBy.TotalJogs, x => x.Jogs.Count() },
            };
    }
}
