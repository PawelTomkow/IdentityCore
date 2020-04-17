namespace Identity.Application.Settings
{
    public class SecuritySettings
    {
        public string Key { get; set; } = "!secret";
        public string Issuer { get; set; } = "http://127.0.0.1:18766";
        public int ExpiryMinutes { get; set; } = 3;
    }
}