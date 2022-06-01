using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Services
{
    public static class IServiceCollectionExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IJogService, JogService>();
            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
