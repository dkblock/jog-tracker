using JogTracker.Configuration;
using JogTracker.Entities;
using JogTracker.Models.Constants;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace JogTracker.Database
{
    public class DataInitializer
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DataInitializer(UserManager<UserEntity> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task Initialize()
        {
            await InitializeRoles();
            await InitializeAdministrator();
        }

        public async Task InitializeRoles()
        {
            var roles = new[] { Roles.Administrator, Roles.User };

            foreach (var role in roles)
            {
                if (await _roleManager.FindByNameAsync(role) == null)
                    await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public async Task InitializeAdministrator()
        {
            var administratorUsername = "administrator";
            var administratorPassword = _configuration.IdentitySettings.AdministratorPassword;

            if (await _userManager.FindByNameAsync(administratorUsername) == null)
            {
                var administrator = new UserEntity
                {
                    FirstName = "Administrator",
                    LastName = "Administrator",
                    UserName = administratorUsername,
                };

                await _userManager.CreateAsync(administrator, administratorPassword);
                await _userManager.AddToRoleAsync(administrator, Roles.Administrator);
            }
        }
    }
}
