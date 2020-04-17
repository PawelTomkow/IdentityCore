using System.Threading.Tasks;
using Identity.Core.Models;

namespace Identity.Core.Repository
{
    public interface ITokenRepository
    {
        Task AddAsync(Token token);
        Task<int> GetUserIdAsync(string refreshToken);
    }
}