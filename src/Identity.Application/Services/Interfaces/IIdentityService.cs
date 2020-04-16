using System.Threading.Tasks;
using Identity.Application.Commands;

namespace Identity.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginCommand loginCommand);
        Task RegisterAsync(RegisterCommand registerCommand);
    }
}