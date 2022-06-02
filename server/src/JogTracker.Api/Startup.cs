using JogTracker.Api;
using JogTracker.Configuration;
using JogTracker.Database;
using JogTracker.Identity;
using JogTracker.Mappers;
using JogTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IConfiguration = JogTracker.Configuration.IConfiguration;

namespace JogTracker.Server
{
    public class Startup
    {
        public Startup() { }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();

            services.AddConfiguration();
            services.AddIdentityServices();
            services.AddDatabase();
            services.AddRepositoryServices();
            services.AddCoreServices();
            services.AddMappers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseCors(builder =>
            {
                builder.WithOrigins(configuration.ClientUrl);
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowCredentials();
            });

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseIdentityMiddleware();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
