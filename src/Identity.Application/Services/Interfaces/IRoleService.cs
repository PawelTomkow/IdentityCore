using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Commands.Role;
using Identity.Core.Models;

namespace Identity.Application.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetAllRolesAsync();
        public Task<Role> Get(GetByIdRoleCommand command);
        public Task AddAsync(AddRoleCommand command);
        public Task UpdateAsync(UpdateRoleCommand command);
        public Task DeleteAsync(DeleteRoleCommand command);
    }
}