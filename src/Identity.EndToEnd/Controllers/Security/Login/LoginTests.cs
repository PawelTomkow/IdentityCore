using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Identity.EndToEnd.Controllers.Security.Login
{
    public class LoginTests : ControllerTestsBase
    {
        [Fact]
        public async Task given_correct_loginAndPassword_to_loginEndpoint_should_return_200()
        {
            var payload = LoginDataBuilder.Generate_Correct_LoginAndPassword_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task given_empty_login_to_loginEndpoint_should_return_401()
        {
            //arrange
            var payload = LoginDataBuilder.Generate_Invalid_LoginAndPassword_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_empty_loginAndPassword_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Empty_LoginAndPassword_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_empty_password_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Empty_Password_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_invalid_login_to_loginEndpoint_should_return_401()
        {
            //arrange
            var payload = LoginDataBuilder.Generate_Invalid_Login_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_invalid_loginAndPassword_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Invalid_LoginAndPassword_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_invalid_password_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Invalid_Password_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_null_login_to_loginEndpoint_should_return_401()
        {
            //arrange
            var payload = LoginDataBuilder.Generate_Null_Login_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_null_loginAndPassword_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Null_LoginAndPassword_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }

        [Fact]
        public async Task given_null_password_to_loginEndpoint_should_return_401()
        {
            var payload = LoginDataBuilder.Generate_Null_Password_LoginCommand();

            //act
            var response = await Client.PostAsync("security/login", payload);

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}