using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Services.Interfaces;
using Identity.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("[controller]/[action]")]
    public class SecurityController : BaseApiController
    {
        private readonly IIdentityService _identityService;
        private readonly ITokenService _tokenService;

        public SecurityController(IIdentityService identityService,
            ITokenService tokenService)
        {
            _identityService = identityService;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            var idRequest = RequestExtension.GenerateIdRequest();
            command.IdRequest = idRequest;
            
            await _identityService.LoginAsync(command);
            
            var token = _tokenService.GetToken(idRequest);
            
            return Ok(token);
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            await _identityService.RegisterAsync(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            await _tokenService.RefreshTokenAsync(refreshTokenCommand);
            return Ok();
        }
    }
}