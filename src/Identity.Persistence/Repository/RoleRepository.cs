using System.Collections.Generic;
using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Core.Repository;
using Identity.Persistence.Context;
using Identity.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityContext _context;
        private const string DefaultNameUser = "user"; 

            public RoleRepository(IdentityContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(int id)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == id);
        }

        public async Task<Role> GetAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

        public async Task AddAsync(Role role)
        {
            var ctxRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == role.Name);
            if (!(ctxRole is null))
            {
                throw new RepositoryException($"Role {role.Name} is exist.");
            }

            await _context.Roles.AddAsync(new Role{Name = role.Name, Value = 1});
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Role role)
        {
            var ctxRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == role.RoleId);
            if (ctxRole is null)
            {
                throw new RepositoryException($"Role with {role.RoleId} not exist.");
            }

            ctxRole.Name = role.Name;
            ctxRole.Value = role.Value;
            _context.Roles.Update(ctxRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            var ctxRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == role.RoleId);
            if (ctxRole is null)
            {
                throw new RepositoryException($"Role with {role.RoleId} not exist.");
            }

            _context.Roles.Remove(ctxRole);
            await _context.SaveChangesAsync();
        }

        public async Task<Role> GetDefaultAsync()
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == DefaultNameUser);
        }
    }
}