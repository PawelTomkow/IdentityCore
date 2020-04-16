using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.DTOs;

namespace Identity.Application.Services.Interfaces
{
    public interface ITokenService
    {
        //TODO: Change to void and get token from cache!
        public Task GenerateTokenAsync(GetTokenCommand tokenCommand);
        public Task RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand);
        public TokenDto GetToken(string key);
    }
}