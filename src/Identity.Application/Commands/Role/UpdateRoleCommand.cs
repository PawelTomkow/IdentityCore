
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands.Role
{
    public class UpdateRoleCommand : ICommand
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string NewName { get; set; }
    }
}