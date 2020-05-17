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
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IdentityContext _context;

        public IdentityRepository(IdentityContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(r => r.Roles)
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetAsync(string userName)
        {
            return await _context.Users.Where(user => user.Username == userName).FirstOrDefaultAsync();
        }

        public async Task<User> GetByMailAsync(string mail)
        {
            return await _context.Users.Where(user => user.Email.Equals(mail)).FirstOrDefaultAsync();
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(User user)
        {
            var contextUser = await _context.Users
                .Include(r=>r.Roles)
                .Where(usr => usr.Id == user.Id)
                .FirstOrDefaultAsync();
            
            if (contextUser != null)
            {
                contextUser.SetEmail(user.Email);
                contextUser.SetPassword(user.Password, user.Salt);
                contextUser.SetRole(user.Roles);

                _context.Users.Update(contextUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserRolesAsync(User user, IEnumerable<Role> roles)
        {
            var contextUser = await _context.Users
                .Include(r => roles)
                .Where(u => u.Id == user.Id)
                .FirstOrDefaultAsync();

            if (contextUser is null)
            {
                throw new RepositoryException($"User id: {user.Id} not found.");
            }

            contextUser.SetRole(roles);
            _context.Users.Update(contextUser);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            var userToDelete = await _context.Users.Where(usr => usr.Id == user.Id).FirstOrDefaultAsync();

            if (userToDelete == null)
            {
                throw new RepositoryException("User not exist.");
            }
            
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }    

        public async Task<IEnumerable<Role>> GetUserRoleAsync(int tokenCommandUserId)
        {
            var result = await _context.Users
                .Include(r => r.Roles)
                .Where(usr => usr.Id == tokenCommandUserId)
                .FirstOrDefaultAsync();
            
            return result.Roles?.ToArray();
        }
    }
}