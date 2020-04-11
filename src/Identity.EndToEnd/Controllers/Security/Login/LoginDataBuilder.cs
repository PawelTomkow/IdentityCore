using System.Net.Http;
using Identity.EndToEnd.Controllers.Extentions;
using Identity.Infrastructure.Commands;

namespace Identity.EndToEnd.Controllers.Security.Login
{
    public static class LoginDataBuilder
    {
        public static StringContent Generate_Invalid_Login_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.IncorrectLogin,
                Password = SecurityConsts.CorrectPassword
            });

        public static StringContent Generate_Invalid_LoginAndPassword_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.IncorrectLogin,
                Password = SecurityConsts.IncorrectPassword
            });

        public static StringContent Generate_Invalid_Password_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.CorrectLogin,
                Password = SecurityConsts.IncorrectPassword
            });

        public static StringContent Generate_Correct_LoginAndPassword_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.CorrectLogin,
                Password = SecurityConsts.CorrectPassword
            });

        public static StringContent Generate_Empty_Login_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = string.Empty,
                Password = SecurityConsts.CorrectPassword
            });

        public static StringContent Generate_Empty_Password_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.CorrectLogin,
                Password = string.Empty
            });

        public static StringContent Generate_Empty_LoginAndPassword_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = string.Empty,
                Password = string.Empty
            });

        public static StringContent Generate_Null_Login_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = null,
                Password = SecurityConsts.CorrectPassword
            });

        public static StringContent Generate_Null_Password_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = SecurityConsts.CorrectLogin,
                Password = null
            });

        public static StringContent Generate_Null_LoginAndPassword_LoginCommand() =>
            PayloadBuilder.GetPayload(new LoginCommand
            {
                Login = null,
                Password = null
            });
    }
}