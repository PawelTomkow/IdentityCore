using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAll();
        public Task<Role> Get(int id);
        public Task<Role> Get(string name);
        public Task Add(Role role);
        public Task Update(Role role);
        public Task Delete(Role role);
    }
}