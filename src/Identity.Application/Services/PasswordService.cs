using System.Threading.Tasks;
using Identity.Application.Commands.Management;
using Identity.Application.Exceptions;
using Identity.Application.Services.Interfaces;
using Identity.Core.Models;
using Identity.Core.Repository;

namespace Identity.Application.Services
{
    public class PasswordService : IPasswordService 
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IEncrypter _encrypter;

        public PasswordService(IIdentityRepository identityRepository, IEncrypter encrypter)
        {
            _identityRepository = identityRepository;
            _encrypter = encrypter;
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
            user.SetPassword(user.Password, user.Password);
            await _identityRepository.EditPasswordAsync(user);
        }
    }
}