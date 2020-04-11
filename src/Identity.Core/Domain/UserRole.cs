namespace Identity.Core.Domain
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int IdUser { get; set; }
        public User User { get; set; }
    }
}