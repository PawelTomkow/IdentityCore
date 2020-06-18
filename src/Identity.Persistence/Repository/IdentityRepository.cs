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
        private readonly IUserRoleRepository _userRoleRepository;

        public IdentityRepository(IdentityContext context, IUserRoleRepository userRoleRepository)
        {
            _context = context;
            _userRoleRepository = userRoleRepository;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserRole)
                .ThenInclude(r=> r.Role).ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.Where(user => user.UserId == id).FirstOrDefaultAsync();
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
            var newUser = user; 
            
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();

            await _userRoleRepository.SetDefaultRoleAsync(newUser.UserId);
        }

        public async Task EditAsync(User user)
        {
            var contextUser = await _context.Users.Where(usr => usr.UserId == user.UserId).FirstOrDefaultAsync();
            if (contextUser != null)
            {
                contextUser.SetEmail(user.Email);
                contextUser.SetPassword(user.Password, user.Salt);
                contextUser.SetRole(user.UserRole);

                _context.Users.Update(contextUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditPasswordAsync(User user)
        {
            var contextUser = await _context.Users.Where(usr => usr.UserId == user.UserId).FirstOrDefaultAsync();
            if (contextUser != null)
            {
                contextUser.SetPassword(user.Password, user.Salt);

                _context.Users.Update(contextUser);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserRolesAsync(User user, IEnumerable<Role> roles)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAsync(User user)
        {
            var userToDelete = await _context.Users.Where(usr => usr.UserId == user.UserId).FirstOrDefaultAsync();

            if (userToDelete == null)
            {
                throw new RepositoryException("User is null");
            }
            
            _context.Users.Remove(userToDelete);
            await _context.SaveChangesAsync();
        }    

        public async Task<IEnumerable<Role>> GetUserRoleAsync(int tokenCommandUserId)
        {
            var result = await _context.Users
                .Include(r=> r.UserRole)
                .ThenInclude(r => r.Role)
                .Where(usr => usr.UserId == tokenCommandUserId)
                .FirstOrDefaultAsync();
            return result.UserRole?.Select(r => r.Role);
        }
    }
}