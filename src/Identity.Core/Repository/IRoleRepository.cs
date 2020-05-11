﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllAsync();
        public Task<Role> GetAsync(int id);
        public Task<Role> GetAsync(string name);
        public Task AddAsync(Role role);
        public Task EditAsync(Role role);
        public Task DeleteAsync(Role role);
    }
}