using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Mappers
{
    public static class IServiceCollectionExtension
    {
        public static void AddMappers(this IServiceCollection services)
        {
            services.AddScoped<IJogMapper, JogMapper>();
            services.AddScoped<IUserMapper, UserMapper>();
        }
    }
}
