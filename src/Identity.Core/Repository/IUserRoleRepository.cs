using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface IUserRoleRepository
    {
        public Task AddAsync(int roleId, int userId);
        public Task AddRangeAsync(IEnumerable<int> roleIds, int userId);
        public IEnumerable<Role> GetRolesAsync(User user);
        public IEnumerable<User> GetUsersAsync(Role role);
        public Task SetDefaultRoleAsync(int userId);
    }
}