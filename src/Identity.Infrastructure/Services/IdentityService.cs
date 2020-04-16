using System;
using System.Threading.Tasks;
using Identity.Core.Domain;
using Identity.Core.Repository;
using Identity.Infrastructure.Commands;
using Identity.Infrastructure.Exceptions;
using Identity.Infrastructure.Services.Interfaces;

namespace Identity.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly ITokenService _tokenService;
        private readonly IEncrypter _encrypter;
        private readonly IIdentityRepository _identityRepository;

        public IdentityService(IIdentityRepository identityRepository,
            IEncrypter encrypter,
            ITokenService tokenService)
        {
            _identityRepository = identityRepository;
            _encrypter = encrypter;
            _tokenService = tokenService;
        }

        public async Task LoginAsync(LoginCommand loginCommand)
        {
            var user = await _identityRepository.GetAsync(loginCommand.Login);
            if (user == null) throw new IdentityExceptions(ErrorCodes.InvalidCredentials, "Invalid credentials");

            var hash = _encrypter.GetHash(loginCommand.Password, user.Salt);
            if (user.Password != hash)
                throw new IdentityExceptions(ErrorCodes.InvalidCredentials, "Invalid credentials");
            await _tokenService.GenerateTokenAsync(new GetTokenCommand
            {
                UserId = user.Id
            });
        }

        public async Task RegisterAsync(RegisterCommand registerCommand)
        {
            var user = await _identityRepository.GetAsync(registerCommand.Email);
            if(user != null)
            {
                throw new IdentityExceptions($"User with email: '{registerCommand.Email}' already exists.");
            }

            var salt = _encrypter.GetSalt(registerCommand.Password);
            var hash = _encrypter.GetHash(registerCommand.Password, salt);
            user = new User(registerCommand.Email,  registerCommand.Username, hash, salt);
            await _identityRepository.AddAsync(user);
        }
    }
}