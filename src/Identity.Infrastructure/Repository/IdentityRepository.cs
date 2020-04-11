using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Domain;
using Identity.Core.Repository;

namespace Identity.Infrastructure.Repository
{
    public class IdentityRepository : IIdentityRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetAsync(string login)
        {
            throw new NotImplementedException();
        }

        public async Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task EditAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetUserRoleAsync(int tokenCommandUserId)
        {
            throw new NotImplementedException();
        }
    }
}