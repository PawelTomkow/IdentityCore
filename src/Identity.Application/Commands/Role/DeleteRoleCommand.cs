
using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands.Role
{
    public class DeleteRoleCommand : ICommand
    {
        [Required]
        public int Id { get; set; }
    }
}