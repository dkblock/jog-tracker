using JogTracker.Common.Constants;
using JogTracker.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JogTracker.Repository
{
    public interface IUsersRepository
    {
        Task Create(UserEntity user, string password);
        Task<bool> IsExistByUserName(string username);
        Task<bool> IsPasswordValid(string username, string password);
        Task<UserEntity> GetByUserName(string username);
        Task<string> GetRole(string userId);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly UserManager<UserEntity> _userManager;

        public UsersRepository(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task Create(UserEntity user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, Roles.User);
        }

        public async Task<bool> IsExistByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        public async Task<bool> IsPasswordValid(string username, string password)
        {
            var user = await GetByUserName(username);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<UserEntity> GetByUserName(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<string> GetRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);

            return roles.Contains(Roles.Administrator) ? Roles.Administrator : Roles.User;
        }
    }
}
