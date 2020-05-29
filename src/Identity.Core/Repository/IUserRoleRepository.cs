using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface IUserRoleRepository
    {
        public Task AddAsync(Role role, User user);
        public IEnumerable<Role> GetRolesAsync(User user);
        public IEnumerable<User> GetUsersAsync(Role role);
    }
}