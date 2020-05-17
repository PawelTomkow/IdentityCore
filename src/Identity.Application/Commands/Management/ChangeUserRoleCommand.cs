using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Identity.Application.DTOs;

namespace Identity.Application.Commands.Management
{
    public class ChangeUserRoleCommand
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public IEnumerable<RoleDto> Roles { get; set; }
    }
}