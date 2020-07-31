using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.Services.Interfaces;
using Identity.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    public class PasswordController : BaseApiController
    {
        private readonly IPasswordService _passwordService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public PasswordController(IPasswordService passwordService, JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _passwordService = passwordService;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            command.UserId = int.Parse(GetUser(RequestExtensions.GetToken(Request.Headers["Authorization"].ToString())) ?? string.Empty) ;
            await _passwordService.ChangePassword(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordCommand command)
        {
            await _passwordService.ResetPassword(command);
            return Accepted();
        }
        
        private string? GetUser(string token)
        {
            var decodedToken = _jwtSecurityTokenHandler.ReadJwtToken(token);
            return decodedToken.Payload["unique_name"].ToString();
        }
    }
}