using JogTracker.Api.Validators;
using JogTracker.Configuration;
using JogTracker.Database;
using JogTracker.Entities;
using JogTracker.Identity;
using JogTracker.Mappers;
using JogTracker.Repository;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace JogTracker.Api
{
    public static class ServiceLocator
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<IAccountValidator, AccountValidator>();
            services.AddScoped<IJogsValidator, JogsValidator>();

            services.AddMediatR(typeof(Startup));
            services.AddSingleton(AutoMapperConfig.Initialize());
        }

        public static void AddConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IConfiguration, Configuration.Configuration>();
        }

        public static void AddDatabase(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var configuration = serviceProvider.GetService<IConfiguration>();
            var connectionString = configuration.ConnectionString;

            services.AddDbContext<ApplicationContext>(options => options.EnableSensitiveDataLogging().UseSqlServer(connectionString));

            InitializeDatabase(services);
        }

        private static void InitializeDatabase(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            new DataInitializer(serviceProvider).Initialize().Wait();
        }

        public static void AddIdentity(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var settings = serviceProvider.GetService<IConfiguration>().IdentitySettings;

            services.AddIdentity<UserEntity, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 4;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(settings.Secret)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IJogsRepository, JogsRepository>();
            services.AddScoped<IRefreshTokensRepository, RefreshTokensRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
        }
    }
}
