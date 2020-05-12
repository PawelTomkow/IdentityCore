using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands.Role
{
    public class AddRoleCommand : ICommand
    {
        [Required]
        public string Name { get; set; }
    }
}