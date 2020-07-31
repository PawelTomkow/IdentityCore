using Identity.Application.Commands;

namespace Identity.Controllers
{
    public class ResetPasswordCommand : ICommand
    {
        public string Email { get; set; }
    }
}