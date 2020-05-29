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
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IdentityContext _context;

        public UserRoleRepository(IdentityContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(Role role, User user)
        {
            var ctxUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            var ctxRole =  await _context.Roles
                .FirstOrDefaultAsync(r => r.IdRole == role.IdRole);

            if (ctxUser is null)
            {
                throw new RepositoryException("Not found user");
            }

            if (ctxRole is null)
            {
                throw new RepositoryException("Not found role");
            }

            var userRole = new UserRole
            {
                Role = ctxRole,
                User = ctxUser
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Role> GetRolesAsync(User user)
        {
            var ctxUserRoles = _context.UserRoles.
                Include(u => u.User)
                .Where(u => u.UserId == user.Id);
            
            if (ctxUserRoles is null)
            {
                throw new RepositoryException("Not found roles.");
            }

            return ctxUserRoles.Select(r => r.Role);
        }

        public IEnumerable<User> GetUsersAsync(Role role)
        {
            var ctxUserRoles = _context.UserRoles
                .Include(u => u.Role)
                .Where(r => r.RoleId == role.IdRole);
            
            if (ctxUserRoles is null)
            {
                throw new RepositoryException("Not found users.");
            }

            return ctxUserRoles.Select(u => u.User);
        }
    }
}