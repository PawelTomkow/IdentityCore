using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Domain;

namespace Identity.Core.Repository
{
    public interface IIdentityRepository : IRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();
        public Task<User> GetAsync(int id);
        public Task<User> GetAsync(string login);
        public Task AddAsync(User user);
        public Task EditAsync(User user);
        public Task DeleteAsync(User user);
        Task<IEnumerable<Role>> GetUserRoleAsync(int tokenCommandUserId);
    }
}