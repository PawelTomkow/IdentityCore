using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Core.Repository;

namespace Identity.Persistence.Repository
{
    public class RoleRepository : IRoleRepository
    {
        public RoleRepository()
        {
            
        }

        public async Task<List<Role>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Role> Get(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task Add(Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task Update(Role role)
        {
            throw new System.NotImplementedException();
        }

        public async Task Delete(Role role)
        {
            throw new System.NotImplementedException();
        }
    }
}