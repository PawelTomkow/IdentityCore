using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.DTOs;
using Identity.Core.Models;

namespace Identity.Application.Services.Interfaces
{
    public interface IManagementService
    {
        public Task<IEnumerable<UserDto>> GetAllUsers();
    }
}