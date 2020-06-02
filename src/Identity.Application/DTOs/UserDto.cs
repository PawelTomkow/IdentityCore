using System.Collections.Generic;

namespace Identity.Application.DTOs
{
    public class UserDto
    {
        public int UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
        public IEnumerable<RoleDto> UserRoles { get; protected set; }
    }
}