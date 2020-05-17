using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Authorize(Roles = "superuser")]
    [Route("auth/[controller]/[action]")]
    public class ManagementController : BaseApiController
    {
        private readonly IManagementService _service;

        public ManagementController(IManagementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _service.GetAllUsers();

            return Ok(result);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeUserRole([FromBody] ChangeUserRoleCommand command)
        {
            await _service.ChangeUserRoleAsync(command);
            return Ok();
        }
        
    }
}