using System.Threading.Tasks;
using Identity.Application.Commands;
using Identity.Application.Exceptions;
using Identity.Application.Services.Interfaces;
using Identity.Core.Models;
using Identity.Core.Repository;

namespace Identity.Application.Services
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
                IdRequest = loginCommand.IdRequest,
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