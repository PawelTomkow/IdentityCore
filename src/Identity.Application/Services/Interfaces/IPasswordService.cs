using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Controllers;

namespace Identity.Application.Services.Interfaces
{
    public interface IPasswordService
    {
        Task ChangePassword(ChangePasswordCommand command);
        Task ResetPassword(ResetPasswordCommand command);
    }
}