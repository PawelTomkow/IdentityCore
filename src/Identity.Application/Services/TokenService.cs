using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.DTOs;
using Identity.Application.Extensions;
using Identity.Application.Services.Interfaces;
using Identity.Application.Settings;
using Identity.Core.Models;
using Identity.Core.Repository;
using Identity.Persistence.Cache;
using Identity.Persistence.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Identity.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly ICache _cache;
        private readonly SecuritySettings _securitySettings;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IIdentityRepository _userService;

        public TokenService(IIdentityRepository userService,
            SecuritySettings securitySettings,
            IPasswordHasher<User> passwordHasher,
            ICache cache)
        {
            _userService = userService;
            _securitySettings = securitySettings;
            _passwordHasher = passwordHasher;
            _cache = cache;
        }

        public async Task GenerateTokenAsync(GetTokenCommand tokenCommand)
        {
            var now = DateTime.UtcNow;
            var userClaims = GetNamesUserRoles(await _userService.GetUserRoleAsync(tokenCommand.UserId));
            var userRoles = userClaims as string[] ?? userClaims?.ToArray();
            var claims = PrepareClaims(tokenCommand.UserId, now, userRoles);
            var expires = GetExperienceTime(now);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.Key)),
                SecurityAlgorithms.HmacSha256);


            var jwt = new JwtSecurityToken(
                _securitySettings.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = await GenerateRefreshTokenAsync(tokenCommand.UserId);
            var jsonToken = ParsingTokenToJson(tokenCommand, token, expires, refreshToken, userRoles);

            _cache.Add(GenerateCacheObject(tokenCommand.IdRequest, jsonToken));
        }

        public async Task RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand)
        {
            throw new NotImplementedException();
        }

        public TokenDto GetToken(string key)
        {
            return _cache.Get(key)?.Value as TokenDto;
        }

        private static string ParsingTokenToJson(GetTokenCommand tokenCommand, string token, DateTime expires,
            string refreshToken, IEnumerable<string> userClaims)
        {
            var jsonToken = new TokenDto
            {
                IdUser = tokenCommand.UserId,
                IdSession = Guid.NewGuid().ToString(),
                Token = token,
                ExperienceTime = expires.ToTimestamp(),
                RefreshToken = refreshToken,
                Claims = userClaims.ToList()
            }.ToJSON();
            return jsonToken;
        }

        private IEnumerable<Claim> PrepareClaims(int userId, DateTime nowTime, IEnumerable<string> userRoles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowTime.ToTimestamp().ToString(), ClaimValueTypes.Integer64)
            };

            if(userRoles != null)
                claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            return claims.ToArray();
        }

        private static IEnumerable<string> GetNamesUserRoles(IEnumerable<Role> roles)
        {
            return roles?.Select(role => role.Name);
        }

        private DateTime GetExperienceTime(DateTime now)
        {
            return now.AddMinutes(_securitySettings.ExpiryMinutes);
        }

        private async Task<string> GenerateRefreshTokenAsync(int userId)
        {
            return _passwordHasher.HashPassword(await _userService.GetAsync(userId), Guid.NewGuid().ToString())
                .Replace("+", string.Empty)
                .Replace("=", string.Empty)
                .Replace("/", string.Empty);
        }

        private CacheObject GenerateCacheObject(string key, string value)
        {
            return new CacheObject
            {
                Key = key,
                Value = value
            };
        }
    }
}