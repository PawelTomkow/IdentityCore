using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.Exceptions;
using Identity.Application.Services.Interfaces;
using Identity.Controllers;
using Identity.Core.Models;
using Identity.Core.Repository;

namespace Identity.Application.Services
{
    public class PasswordService : IPasswordService 
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IEncrypter _encrypter;
        private readonly IMailSender _mailSender;

        public PasswordService(IIdentityRepository identityRepository, IEncrypter encrypter, IMailSender mailSender)
        {
            _identityRepository = identityRepository;
            _encrypter = encrypter;
            _mailSender = mailSender;
        }
        
        public async Task ChangePassword(ChangePasswordCommand command)
        {
            var user = await _identityRepository.GetAsync(command.UserId);
            if (user is null)
            {
                throw new IdentityExceptions("User not found");
            }
            
            var salt = _encrypter.GetSalt(command.NewPassword);
            var hash = _encrypter.GetHash(command.NewPassword, salt);
            user.SetPassword(hash, salt);
            await _identityRepository.EditPasswordAsync(user);
        }

        public async Task ResetPassword(ResetPasswordCommand command)
        {
            var user = await _identityRepository.GetByMailAsync(command.Email);
            if (user is null)
            {
                throw new IdentityExceptions("User with this email not found");
            }

            var randomPassword = StringExtension.RandomString(8);
            
            var salt = _encrypter.GetSalt(randomPassword);
            var hash = _encrypter.GetHash(randomPassword, salt);
            user.SetPassword(user.Password, user.Password);
            await _identityRepository.EditPasswordAsync(user);
            
            await _mailSender.SendEmail(command.Email, randomPassword);
        }
    }
}