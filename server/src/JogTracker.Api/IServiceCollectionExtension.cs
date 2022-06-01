using JogTracker.Api.Core;
using JogTracker.Api.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace JogTracker.Api
{
    public static class IServiceCollectionExtension
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountHandler, AccountHandler>();
            services.AddScoped<IJogHandler, JogHandler>();

            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IJogValidator, JogValidator>();
        }
    }
}
