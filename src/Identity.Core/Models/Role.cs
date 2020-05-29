using System.Collections.Generic;

namespace Identity.Core.Models
{
    public class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public ICollection<UserRole> UserRole { get; set; }
    }
}