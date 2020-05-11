using System.Collections.Generic;
using System.Linq;
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

        public RoleRepository(IdentityContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<Role> GetAsync(int id)
        {
            var response = await _context.Roles.Where(i => i.IdRole == id).FirstOrDefaultAsync();
            if (response is null)
            {
                throw new RepositoryException($"User {id} not found.");
            }

            return response;
        }

        public async Task<Role> GetAsync(string name)
        {
            var response = await _context.Roles.Where(i => i.Name == name).FirstOrDefaultAsync();
            if (response is null)
            {
                throw new RepositoryException($"Role {name} not found.");
            }

            return response;
        }

        public async Task AddAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Role role)
        {
            var ctx = await _context.Roles.Where(role1 => role1.IdRole == role.IdRole).FirstOrDefaultAsync();
            if (ctx is null)
            {
                throw new RepositoryException($"Role {role.Name} not exist.");
            }

            ctx.Name = role.Name;
            ctx.Value = role.Value;

            _context.Roles.Update(ctx);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Role role)
        {
            var ctx = await _context.Roles.Where(role1 => role1.IdRole == role.IdRole).FirstOrDefaultAsync();
            if (ctx is null)
            {
                throw new RepositoryException($"Role {role.Name} not exist.");
            }

            _context.Roles.Remove(ctx);
            await _context.SaveChangesAsync();
        }
    }
}