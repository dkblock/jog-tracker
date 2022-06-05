using JogTracker.Models.DTO;
using JogTracker.Models.DTO.Users;
using MediatR;

namespace JogTracker.Models.Requests.Users
{
    public class GetUsersQuery : IRequest<PageResponse<User>>
    {
        public GetUsersQuery(string searchText, string role, int pageSize, int pageIndex, UsersSortBy sortBy, bool sortByDesc)
        {
            SearchText = searchText?.ToLower();
            Role = role;
            PageSize = pageSize;
            PageIndex = pageIndex;
            SortBy = sortBy;
            SortByDesc = sortByDesc;
        }

        public string SearchText { get; set; }
        public string Role { get; set; }
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public UsersSortBy SortBy { get; set; }
        public bool SortByDesc { get; set; }
    }

    public enum UsersSortBy
    {
        FirstName,
        LastName,
        Username,
        Role
    }
}
