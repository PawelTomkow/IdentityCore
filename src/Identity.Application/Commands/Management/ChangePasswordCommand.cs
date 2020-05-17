using System.ComponentModel.DataAnnotations;

namespace Identity.Application.Commands.Management
{
    public class ChangePasswordCommand
    {
        [Required] public int UserId { get; set; }
        [Required] public string NewPassword { get; set; }
    }
}