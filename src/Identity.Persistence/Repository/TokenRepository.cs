﻿using System.Linq;
using System.Threading.Tasks;
using Identity.Core.Models;
using Identity.Core.Repository;
using Identity.Persistence.Context;
using Identity.Persistence.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.Repository
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IdentityContext _context;
        private readonly IIdentityRepository _identityRepository;

        public TokenRepository(IdentityContext context, IIdentityRepository identityRepository)
        {
            _context = context;
            _identityRepository = identityRepository;
        }
        
        public async Task AddAsync(Token token)
        {
            await _context.Tokens.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserIdAsync(string refreshToken)
        {
            var token = await _context.Tokens
                .Include(i => i.User)
                .Where(token1 => token1.RefreshToken.Equals(refreshToken))
                .FirstOrDefaultAsync();
            if (token == null)
            {
                throw new RepositoryException($"Not found refresh token: {refreshToken}");
            }

            return token.User.UserId;
        }

        public async Task AddAsync(Token token, int userId)
        {
            var user = await _identityRepository.GetAsync(userId);
            if (user == null)
            {
                throw new RepositoryException("TokenRepository: not found user.");
            }

            token.User = user;
            await _context.AddAsync(token);
            await _context.SaveChangesAsync();
        }
    }
}