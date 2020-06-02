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

        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;

        public ManagementService(IIdentityRepository repository, IUserRoleRepository userRoleRepository, IMapper mapper)
        {
            _repository = repository;
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        
        public async Task ChangeUserRoleAsync(ChangeUserRoleCommand command)
        {
            await _userRoleRepository.AddRangeAsync(command.RoleIds, command.UserId);
        }

        public async Task ChangePasswordAsync(ChangePasswordCommand command)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> GetAllUsers()
        {
            var users = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }
    }
}