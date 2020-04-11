using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Identity.EndToEnd.Controllers.Security.Registration
{
    public class RegistrationTests : ControllerTestsBase
    {
        private const string Endpoint = "security/registration";

        [Fact]
        public async Task given_correct_LoginMailPassword_to_registration_should_200()
        {
            //arrange
            var payload = RegistrationDataBuilder.Generate_Correct_Registration_RegistrationCommand();
            
            //act
            var response = await Client.PostAsync(Endpoint, payload);
            
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task given_incorrect_LoginMailPassword_to_registration_should_401()
        {
            //arrange
            var payload = RegistrationDataBuilder.Generate_Incorrect_Registration_RegistrationCommand();
            
            //act
            var response = await Client.PostAsync(Endpoint, payload);
            
            //assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}