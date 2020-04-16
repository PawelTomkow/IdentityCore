using System.Collections.Generic;

namespace Identity.Core.Domain
{
    public class Role
    {
        public int IdRole { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        // public ICollection<User> Users { get; set; }
    }
}