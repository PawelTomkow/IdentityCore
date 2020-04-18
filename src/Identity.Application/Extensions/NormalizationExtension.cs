using Identity.Application.Commands;

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
            command.Login = command.Login.ToLower();
        }
    }
}