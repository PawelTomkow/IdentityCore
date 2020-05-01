using Identity.Application.Commands;
using Identity.Application.Commands.Auth.Login;
using Identity.Application.Commands.Auth.Register;

namespace Identity.Application.Extensions
{
    public static class NormalizationExtension
    {
        public static void Normalize(this RegisterCommand registerCommand)
        {
            registerCommand.Username = registerCommand.Username.ToLower();
            registerCommand.Email = registerCommand.Email.ToLower();
        }

        public static void Normalize(this LoginCommand command)
        {
            command.Username = command.Username.ToLower();
        }
    }
}