namespace Identity.Infrastructure.Settings
{
    public class SecuritySettings
    {
        public string Key { get; set; } = "!secret";
        public string Issuer { get; set; } = "Identity";
        public int ExpiryMinutes { get; set; } = 3;
    }
}