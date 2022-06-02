using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Configuration
{
    public static class ServiceLocator
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IConfiguration, Configuration>();
        }
    }
}
