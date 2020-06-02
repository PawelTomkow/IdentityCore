using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands.Role
{
    public class GetByIdRoleCommand : ICommand
    {
        [Required]
        public int RoleId { get; set; }
    }
}