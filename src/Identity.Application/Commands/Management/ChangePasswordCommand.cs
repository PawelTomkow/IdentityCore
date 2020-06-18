using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Application.Commands.Management
{
    public class ChangePasswordCommand
    {
        [NotMapped] public int UserId { get; set; }
        [Required] public string NewPassword { get; set; }
    }
}