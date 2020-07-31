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
        private readonly IRoleRepository _roleRepository;

        public UserRoleRepository(IdentityContext context, IRoleRepository roleRepository)
        {
            _context = context;
            _roleRepository = roleRepository;
        }
        
        public async Task AddAsync(int roleId, int userId)
        {
            var ctxUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (ctxUser is null)
            {
                throw new RepositoryException("Not found user");
            }
            
            var ctxRole =  await _context.Roles
                .FirstOrDefaultAsync(r => r.RoleId == roleId);
            
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

        public async Task AddRangeAsync(IEnumerable<int> roleIds, int userId)
        {
            var ctxUser = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == userId);
            
            if (ctxUser is null)
            {
                throw new RepositoryException("Not found user");
            }

            await RemoveUserRoles(ctxUser);

            foreach (var roleId in roleIds)
            {
                var ctxRole =  await _context.Roles
                    .FirstOrDefaultAsync(r => r.RoleId == roleId);
                
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
            }
            
            await _context.SaveChangesAsync();
        }

        private async Task RemoveUserRoles(User user)
        {
            var userRoles = await GetRolesForUser(user);

            if (userRoles is null)
            {
                return;
            }
            
            _context.UserRoles.RemoveRange(userRoles);
            await _context.SaveChangesAsync();
        }

        private async Task<IEnumerable<UserRole>> GetRolesForUser(User user)
        {
            return await _context.UserRoles.Where(role => role.UserId == user.UserId).ToListAsync();
        }

        public IEnumerable<Role> GetRolesAsync(User user)
        {
            var ctxUserRoles = _context.UserRoles.
                Include(u => u.Role)
                .Where(u => u.UserId == user.UserId);
            
            if (ctxUserRoles is null)
            {
                throw new RepositoryException("Not found roles.");
            }

            return ctxUserRoles.Select(r => r.Role);
        }

        public IEnumerable<User> GetUsersAsync(Role role)
        {
            var ctxUserRoles = _context.UserRoles
                .Include(u => u.User)
                .Where(r => r.RoleId == role.RoleId);
            
            if (ctxUserRoles is null)
            {
                throw new RepositoryException("Not found users.");
            }

            return ctxUserRoles.Select(u => u.User);
        }

        public async Task SetDefaultRoleAsync(int userId)
        {
            var defaultRole = await _roleRepository.GetDefaultAsync();

            var ctxRole = await _context.Roles.FirstOrDefaultAsync(r => r.RoleId == defaultRole.RoleId);
            var ctxUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            
            var userRole = new UserRole()
            {
                User = ctxUser,
                Role = ctxRole
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }
    }
}