using System.Threading.Tasks;
using Identity.Application.Commands.Role;
using Identity.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Controllers
{
    [Route("auth/[controller]/[action]")]
    public class RoleController : BaseApiController
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _roleService.GetAllRolesAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetByIdRoleCommand getByIdRoleCommand)
        {
            var result = await _roleService.Get(getByIdRoleCommand);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddRoleCommand command)
        {
            await _roleService.AddAsync(command);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody]UpdateRoleCommand command)
        {
            await _roleService.UpdateAsync(command);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idRole)
        {
            await _roleService.DeleteAsync(new DeleteRoleCommand {Id = idRole});
            return Ok();
        }
    }
}