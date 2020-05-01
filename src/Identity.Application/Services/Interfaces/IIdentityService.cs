using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Commands.Auth.Login;
using Identity.Application.Commands.Auth.Register;

namespace Identity.Application.Services.Interfaces
{
    public interface IIdentityService
    {
        Task LoginAsync(LoginCommand loginCommand);
        Task RegisterAsync(RegisterCommand registerCommand);
    }
}