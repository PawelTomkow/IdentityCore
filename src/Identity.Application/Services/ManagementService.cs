using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Application.Commands.Management;
using Identity.Application.DTOs;
using Identity.Application.Exceptions;
using Identity.Application.Services.Interfaces;
using Identity.Core.Models;
using Identity.Core.Repository;

namespace Identity.Application.Services
{
    public class ManagementService : IManagementService
    {
        private readonly IIdentityRepository _repository;
        // private readonly IMapper _mapper;

        public ManagementService(IIdentityRepository repository)
        {
            _repository = repository;
            // _mapper = mapper;
        }
        
        public async Task ChangeUserRoleAsync(ChangeUserRoleCommand command)
        {
            var user = await _repository.GetAsync(command.UserId);
            if (user is null)
            {
                throw new ManagementException($"User id: {command.UserId} not found."); 
            }
            throw new System.NotImplementedException();

            // var roles = _mapper.Map<IEnumerable<Role>>(command.Roles);
        }

        public async Task ChangePasswordAsync(ChangePasswordCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _repository.GetAllAsync();
            throw new System.NotImplementedException();
            // return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}