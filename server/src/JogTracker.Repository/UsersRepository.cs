using JogTracker.Common.Constants;
using JogTracker.Database;
using JogTracker.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace JogTracker.Repository
{
    public interface IUsersRepository
    {
        Task Create(UserEntity user, string password);
        Task<bool> IsExistById(string id);
        Task<bool> IsExistByUserName(string username);
        Task<bool> IsPasswordValid(string username, string password);
        Task<UserEntity> GetById(string id);
        Task<UserEntity> GetByUserName(string username);
        IQueryable<UserEntity> GetQueryable();
        Task Delete(UserEntity user);
        Task Update(UserEntity user);
        Task UpdateRole(UserEntity user, string role);
    }

    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<UserEntity> _userManager;

        public UsersRepository(ApplicationContext context, UserManager<UserEntity> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task Create(UserEntity user, string password)
        {
            await _userManager.CreateAsync(user, password);
            await _userManager.AddToRoleAsync(user, Roles.User);
        }

        public async Task<bool> IsExistById(string id)
        {
            return await GetById(id) != null;
        }

        public async Task<bool> IsExistByUserName(string username)
        {
            return await GetByUserName(username) != null;
        }

        public async Task<bool> IsPasswordValid(string username, string password)
        {
            var user = await GetByUserName(username);

            if (user == null)
                return false;

            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<UserEntity> GetById(string id)
        {
            return await _context.ApplicationUsers
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UserEntity> GetByUserName(string username)
        {
            return await _context.ApplicationUsers
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public IQueryable<UserEntity> GetQueryable()
        {
            return _context.ApplicationUsers
                .AsQueryable()
                .AsNoTracking();
        }

        public async Task Delete(UserEntity user)
        {
            await _userManager.DeleteAsync(user);
        }

        public async Task Update(UserEntity user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _userManager.UpdateAsync(user);
        }

        public async Task UpdateRole(UserEntity user, string role)
        {
            _context.Entry(user).State = EntityState.Modified;

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
