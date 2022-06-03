using JogTracker.Common.Settings;
using System;

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
        public string ConnectionString => GetValue("ConnectionStrings__Default");
        public string ClientUrl => GetValue("Client__Url");

        public IdentitySettings IdentitySettings => new IdentitySettings
        {
            Secret = GetValue("Identity__Secret"),
            AccessTokenLifetimeInMinutes = int.Parse(GetValue("Identity__AccessTokenLifetimeInMinutes")),
            RefreshTokenLifetimeInDays = int.Parse(GetValue("Identity__RefreshTokenLifetimeInDays")),
            AdministratorPassword = GetValue("Identity__AdministratorPassword")
        };

        private string GetValue(string name)
        {
            return Environment.GetEnvironmentVariable(name);
        }
    }    
}
