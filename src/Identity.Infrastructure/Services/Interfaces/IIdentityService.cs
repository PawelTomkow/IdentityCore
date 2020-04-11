using System.Threading.Tasks;
using Identity.Infrastructure.Commands;

namespace Identity.Infrastructure.Services.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginCommand loginCommand);
        Task RegisterAsync(RegisterCommand registerCommand);
    }
}