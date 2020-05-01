using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Application.Commands.Auth.Login
{
    public class LoginCommand : ICommand
    {
        [NotMapped] public string IdRequest { get; set; }

        [Required] public string Username { get; set; }

        [Required] public string Password { get; set; }
    }
}