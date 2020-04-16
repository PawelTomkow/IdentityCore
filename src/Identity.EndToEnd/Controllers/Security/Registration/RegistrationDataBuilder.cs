using System.Net.Http;
using Identity.Application.Commands;
using Identity.EndToEnd.Controllers.Extentions;

namespace Identity.EndToEnd.Controllers.Security.Registration
{
    public class RegistrationDataBuilder
    {
        public static StringContent Generate_Correct_Registration_RegistrationCommand() =>
            PayloadBuilder.GetPayload(new RegisterCommand()
            {
                Email = RegistrationConst.CorrectEmail,
                Password = RegistrationConst.CorrectPassword,
                Username = RegistrationConst.CorrectPassword
            });

        public static StringContent Generate_Incorrect_Registration_RegistrationCommand() =>
            PayloadBuilder.GetPayload(new RegisterCommand()
            {
                Email = RegistrationConst.IncorrectWithoutAtEmail,
                Password = RegistrationConst.IncorrectPassword,
                Username = RegistrationConst.IncorrectUsername
            });
    }
}