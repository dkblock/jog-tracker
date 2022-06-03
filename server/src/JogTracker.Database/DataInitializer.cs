using JogTracker.Common.Constants;
using JogTracker.Configuration;
using JogTracker.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace JogTracker.Database
{
    public class DataInitializer
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public DataInitializer(ServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _configuration = serviceProvider.GetService<IConfiguration>();
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
