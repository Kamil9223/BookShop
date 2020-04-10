using Ksiegarnia.IServices;
using System;
using System.Linq;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;
using Ksiegarnia.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Ksiegarnia.Responses;
using Ksiegarnia.Requests;
using Ksiegarnia.Exceptions;

namespace Ksiegarnia.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILoggedUserRepository loggedUserRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtService jwtService;

        public UserService(IUserRepository userRepository, ILoggedUserRepository loggedUserRepository,
            IEncrypter encrypter, IJwtService jwtService)
        {
            this.userRepository = userRepository;
            this.loggedUserRepository = loggedUserRepository;
            this.encrypter = encrypter;
            this.jwtService = jwtService;
        }

        public async Task<UserResponse> Get(string login)
        {
            var user = await userRepository.GetUser(login);
            if(user == null)
            {
                throw new NotFoundException($"user with login: '{login}' does't exist.");
            }
            var userResponse = new UserResponse()
            {
                UserId = user.UserId,
                Login = user.Login,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Orders = user.Orders
            };

            return userResponse;
        }

        public async Task<AuthenticationResult> Login(string login, string password)
        {
            var user = await userRepository.GetUser(login);
            if (user == null)
            {
                throw new InvalidCredentialsException("Invalid credentials");
            }

            string salt = user.Salt;
            string hash = encrypter.GetHash(password, salt);
            if (user.Password != hash)
            {
                throw new InvalidCredentialsException("Invalid credentials");
            }
            
            var authResult = jwtService.CreateToken(user.Login, "user");

            var refreshToken = new LoggedUser
            {
                RefreshToken = Guid.NewGuid(),
                JwtId = authResult.JwtId,
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
                UserId = user.UserId
            };

            await loggedUserRepository.AddLoggedUser(refreshToken);
            await loggedUserRepository.SaveChanges();

            authResult.RefreshToken = refreshToken.RefreshToken;
            return authResult;
        }

        public async Task<AuthenticationResult> RefreshConnection(string jwtToken, string refreshToken)
        {
            var loggedUser = await loggedUserRepository.GetLoggedUser(Guid.Parse(refreshToken));

            var authResult = jwtService.RefreshToken(jwtToken, loggedUser);

            loggedUser.JwtId = authResult.JwtId;
            await loggedUserRepository.UpdateLoggedUser(loggedUser);
            await loggedUserRepository.SaveChanges();
            authResult.RefreshToken = loggedUser.RefreshToken;

            return authResult;
        }

        public async Task Logout()
        {
            var jwt = jwtService.GetCurrentToken();
            var validatedToken = jwtService.GetPrincipalFromToken(jwt);
            //zbadaj czemu Authorize nie dziala na przedawnione tokeny
            var login = validatedToken.Claims.Single(x => x.Type == "login").Value;
            var userId = (await userRepository.GetUser(login)).UserId;
            var JwtId = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            await loggedUserRepository.RemoveLoggedUser(JwtId);
            await loggedUserRepository.SaveChanges();

            await jwtService.DeactivateCurrentToken();
        }

        public async Task Register(string login, string password, string email, AddressRequest addressRequest = null)
        {
            var user = await userRepository.GetUser(login);
            if (user != null)
            {
                throw new AlreadyExistException($"User with login: '{login}' already exist.");
            }

            user = await userRepository.GetUserByEmail(email);
            if (user != null)
            {
                throw new AlreadyExistException($"User with email: '{email}' already exist.");
            }

            string salt = encrypter.GetSalt();
            string hash = encrypter.GetHash(password, salt);
            user = new User(login, email, hash, salt);

            if (addressRequest != null)
            {
                Address address = new Address(
                        user.UserId,
                        addressRequest.City,
                        addressRequest.Street,
                        addressRequest.HouseNumber,
                        addressRequest.ZipCode,
                        addressRequest.FlatNumber
                    );
                await userRepository.AddAddress(address);
            }
            await userRepository.AddUser(user);
            await userRepository.SaveChanges();
        }
    }
}
