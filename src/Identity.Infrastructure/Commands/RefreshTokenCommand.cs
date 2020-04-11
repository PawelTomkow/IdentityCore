
namespace Identity.Infrastructure.Commands
{
    public class RefreshTokenCommand : ICommand
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; }
        public string IpUser { get; set; }
    }
}