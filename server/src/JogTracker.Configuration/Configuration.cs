using JogTracker.Common.Settings;
using System;
using IAppConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace JogTracker.Configuration
{
    public interface IConfiguration
    {
        string ConnectionString { get; }
        string ClientUrl { get; }
        IdentitySettings IdentitySettings { get; }
    }

    public class Configuration : IConfiguration
    {
        private readonly IAppConfiguration _appConfiguration;

        public Configuration(IAppConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public string ConnectionString => GetValue("ConnectionStrings__Default");
        public string ClientUrl => GetValue("Client__Url");

        public IdentitySettings IdentitySettings => new IdentitySettings
        {
            Secret = GetValue("Identity__Secret"),
            AccessTokenLifetimeInMinutes = int.Parse(GetValue("Identity__AccessTokenLifetimeInMinutes")),
            RefreshTokenLifetimeInDays = int.Parse(GetValue("Identity__RefreshTokenLifetimeInDays")),
            AdministratorUsername = GetValue("Identity__AdministratorUsername"),
            AdministratorPassword = GetValue("Identity__AdministratorPassword"),
        };

        private string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name) ?? _appConfiguration[name];
        }
    }    
}
