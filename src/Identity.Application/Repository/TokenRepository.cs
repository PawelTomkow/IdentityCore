using System.Linq;
using System.Threading.Tasks;
using Identity.Application.Exceptions;
using Identity.Core.Models;
using Identity.Core.Repository;
using Identity.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Identity.Application.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IdentityContext _context;

        public TokenRepository(IdentityContext context)
        {
            _context = context;
        }
        
        public async Task AddAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserIdAsync(string refreshToken)
        {
            var token = await _context.Tokens.Where(token => token.RefreshToken.Equals(refreshToken)).FirstOrDefaultAsync();
            if (token == null)
            {
                throw new RepositoryException($"Not found refresh token: {refreshToken}");
            }

            return token.UserId;
        }
    }
}