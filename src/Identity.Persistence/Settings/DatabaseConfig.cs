namespace Identity.Persistence.Settings
{
    public class DatabaseConfig
    {
        public bool Migrations { get; set; }
        public bool Seeder { get; set; }
        public string Connection { get; set; }
        public string Type { get; set; }
    }
}