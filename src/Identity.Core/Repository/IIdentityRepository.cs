using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface IIdentityRepository : IRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetAsync(int id);
        public Task<User> GetAsync(string userName);
        Task<User> GetByMailAsync(string mail);
        public Task AddAsync(User user);
        public Task EditAsync(User user);
        public Task UpdateUserRolesAsync(User user, IEnumerable<Role> roles);
        public Task DeleteAsync(User user);
        Task<IEnumerable<Role>> GetUserRoleAsync(int tokenCommandUserId);
    }
}