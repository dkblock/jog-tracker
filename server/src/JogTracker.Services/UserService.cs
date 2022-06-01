using JogTracker.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JogTracker.Services
{
    public interface IUserService
    {
        Task<UserEntity> CreateUser(UserEntity user, string password);
        Task<bool> IsUserExistByUsername(string username);
        Task<bool> IsPasswordValid(string username, string password);
        Task<UserEntity> GetByUsername(string username);
        Task<string> GetUserRole(string id);
    }

    public class UserService : IUserService
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserService(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserEntity> CreateUser(UserEntity user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, "user");

            return user;
        }

        public async Task<bool> IsUserExistByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username) != null;
        }

        public async Task<bool> IsPasswordValid(string username, string password)
        {
            var user = await GetByUsername(username);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<UserEntity> GetByUsername(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<string> GetUserRole(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);

            return roles.Contains("administrator") ? "administrator" : "user";
        }
    }
}
