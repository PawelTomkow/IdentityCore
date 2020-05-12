using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Commands.Role;
using Identity.Application.Services.Interfaces;
using Identity.Core.Models;
using Identity.Core.Repository;
using Microsoft.AspNetCore.Authorization;

namespace Identity.Application.Services
{
    [Authorize(Roles = "superuser")]
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return  await _repository.GetAllAsync();
        }

        public async Task<Role> Get(GetByIdRoleCommand command)
        {
            var role = await _repository.GetAsync(command.Id);
            if (role is null)
            {
                throw new RoleException($"Role id: {command.Id} not exist.");
            }

            return role;
        }

        public async Task AddAsync(AddRoleCommand command)
        {
            command.Name = command.Name.ToLower();
            
            var role = await _repository.GetAsync(command.Name);
            if (!(role is null))
            {
                throw new RoleException($"Role name: {command.Name} exist.");
            }

            await _repository.AddAsync(new Role{Name = command.Name});
        }

        public async Task UpdateAsync(UpdateRoleCommand command)
        {
            command.NewName = command.NewName.ToLower();
            
            var role = await _repository.GetAsync(command.Id);
            if (role is null)
            {
                throw new RoleException($"Role id: {command.Id} not exist.");
            }

            await _repository.UpdateAsync(new Role{IdRole = role.IdRole, Name = command.NewName});
        }

        public async Task DeleteAsync(DeleteRoleCommand command)
        {
            var role = await _repository.GetAsync(command.Id);
            if (role is null)
            {
                throw new RoleException($"Role id: {command.Id} not exist.");
            }

            await _repository.DeleteAsync(role);
        }
    }
}