using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Infrastructure.Commands
{
    public class LoginCommand : ICommand
    {
        [NotMapped] public string IdRequest { get; set; }

        [Required] public string Login { get; set; }

        [Required] public string Password { get; set; }
    }
}