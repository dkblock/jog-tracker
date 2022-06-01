namespace JogTracker.Models.Settings
{
    public class IdentitySettings
    {
        public string Secret { get; set; }
        public int AccessTokenLifetimeInMinutes { get; set; }
        public int RefreshTokenLifetimeInDays { get; set; }
        public string Issuer { get; set; }
        public string AdministratorPassword { get; set; }
    }
}
