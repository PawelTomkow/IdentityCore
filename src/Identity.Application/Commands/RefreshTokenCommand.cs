
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Application.Commands
{
    public class RefreshTokenCommand : ICommand
    {
        [Required] public string RefreshToken { get; set; }
        [NotMapped] public string IpUser { get; set; }
        [NotMapped] public string IdRequest { get; set; }
    }
}