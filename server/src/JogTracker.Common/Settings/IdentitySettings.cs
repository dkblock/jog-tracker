namespace JogTracker.Common.Settings
{
    public class IdentitySettings
    {
        public string Secret { get; set; }
        public int AccessTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenLifetimeInDays { get; set; }
        public string AdministratorUsername { get; set; }
        public string AdministratorPassword { get; set; }
    }
}
