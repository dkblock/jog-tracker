using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Services
{
    public static class ServiceLocator
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IJogService, JogService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IQueryHelper, QueryHelper>();
        }
    }
}
