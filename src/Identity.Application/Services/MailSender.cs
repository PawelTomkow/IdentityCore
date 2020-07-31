using System.Threading.Tasks;
using Identity.Application.Services.Interfaces;

namespace Identity.Application.Services
{
    public class MailSender : IMailSender
    {
        public Task SendEmail(string emailAddress, string content)
        {
            throw new System.NotImplementedException();
        }
    }
}