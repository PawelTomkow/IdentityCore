using System.Threading.Tasks;
using Identity.Application.Commands.Management;

namespace Identity.Application.Services.Interfaces
{
    public interface IPasswordService
    {
        Task ChangePassword(ChangePasswordCommand command);
    }
}