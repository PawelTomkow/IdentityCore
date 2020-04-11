using System.Threading.Tasks;
using Identity.Infrastructure.Commands;
using Identity.Infrastructure.DTOs;

namespace Identity.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        //TODO: Change to void and get token from cache!
        public Task GenerateTokenAsync(GetTokenCommand tokenCommand);
        public Task RefreshTokenAsync(RefreshTokenCommand refreshTokenCommand);
        public TokenDto GetToken(string key);
    }
}