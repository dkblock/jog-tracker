using JogTracker.Configuration;
using JogTracker.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Database
{
    public static class IServiceCollectionExtension
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var connectionString = configuration.ConnectionString;

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            InitializeDatabase(services);
        }

        private static void InitializeDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = serviceProvider.GetService<IConfiguration>();

            new DataInitializer(userManager, roleManager, configuration).Initialize().Wait();
        }
    }
}
