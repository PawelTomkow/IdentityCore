
namespace Identity.Application.Commands.Auth.Register
{
    public class RegisterCommand : ICommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}