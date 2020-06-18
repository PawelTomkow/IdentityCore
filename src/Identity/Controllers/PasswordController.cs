using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.Services.Interfaces;
using Identity.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class PasswordController : BaseApiController
    {
        private readonly IPasswordService _passwordService;

        public PasswordController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.UserId = int.Parse(GetUser(RequestExtensions.GetToken(Request.Headers["Authorization"].ToString())) ?? string.Empty) ;
            await _passwordService.ChangePassword(command);
            return Ok();
        }
        
        private string? GetUser(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var decodedToken = handler.ReadJwtToken(token);

            return decodedToken.Payload["unique_name"].ToString();
        }
    }
}