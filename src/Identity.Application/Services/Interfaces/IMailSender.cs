using System.Threading.Tasks;

namespace Identity.Application.Services.Interfaces
{
    public interface IMailSender
    {
        public Task SendEmail(string emailAddress, string content);
    }
}