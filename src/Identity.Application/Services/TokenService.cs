using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Identity.Application.Cache;
using Identity.Application.Commands;
using Identity.Application.Commands.Auth.Login;
using Identity.Application.Commands.Auth.RefreshToken;
using Identity.Application.DTOs;
using Identity.Application.Extensions;
using Identity.Application.Services.Interfaces;
using Identity.Application.Settings;
using Identity.Core.Models;
using Identity.Core.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Identity.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly ICache _cache;
        private readonly IMapper _mapper;
        private readonly SecuritySettings _securitySettings;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IIdentityRepository _identityRepository;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IIdentityRepository identityRepository,
            ITokenRepository tokenRepository,
            SecuritySettings securitySettings,
            IPasswordHasher<User> passwordHasher,
            ICache cache,
            IMapper mapper)
        {
            _identityRepository = identityRepository;
            _tokenRepository = tokenRepository;
            _securitySettings = securitySettings;
            _passwordHasher = passwordHasher;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task GenerateTokenAsync(GetTokenCommand tokenCommand)
        {
            var now = DateTime.UtcNow;
            var userClaims = GetNamesUserRoles(await _identityRepository.GetUserRoleAsync(tokenCommand.UserId));
            var userRoles = userClaims as string[] ?? userClaims?.ToArray();
            var claims = PrepareClaims(tokenCommand.UserId, now, userRoles);
            var expires = GetExperienceTime(now);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securitySettings.Key)),
                SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(
                issuer: _securitySettings.Issuer,
                audience: _securitySettings.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: signingCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);
            var refreshToken = await GenerateRefreshTokenAsync(tokenCommand.UserId);
            var objToken = ParsingTokenToDto(tokenCommand, token, expires, refreshToken, userRoles);
            var jsonToken = ParsingTokenToJson(objToken);

            var tokenDatabaseModel = _mapper.Map<Token>(objToken);

            await _tokenRepository.AddAsync(tokenDatabaseModel,tokenCommand.UserId);
            _cache.Add(GenerateCacheObject(tokenCommand.IdRequest, jsonToken));
        }

        public async Task RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand)
        {
            var userId = await _tokenRepository.GetUserIdAsync(refreshTokenCommand.RefreshToken);
            var tokenCommand = new GetTokenCommand()
            {
                IdRequest = refreshTokenCommand.IdRequest,
                UserId = userId
            };

            await GenerateTokenAsync(tokenCommand);
        }

        public string GetToken(string key)
        {
            return _cache.Get(key)?.Value as string;
        }

        private static TokenDto ParsingTokenToDto(GetTokenCommand tokenCommand, string token, DateTime expires,
            string refreshToken, IEnumerable<string> userClaims)
        {
            var objToken = new TokenDto
            {
                IdSession = tokenCommand.IdRequest,
                AccessToken = token,
                ExperienceTime = expires.ToTimestamp(),
                RefreshToken = refreshToken,
                Claims = userClaims?.ToList()
            };
            return objToken;
        }

        private static string ParsingTokenToJson(TokenDto tokenDto)
        {
            return tokenDto.ToJSON();
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
            return _passwordHasher.HashPassword(await _identityRepository.GetAsync(userId), Guid.NewGuid().ToString())
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