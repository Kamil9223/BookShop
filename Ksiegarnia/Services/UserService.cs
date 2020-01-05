using Ksiegarnia.IServices;
using System;
using System.Linq;
using Ksiegarnia.DTO;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;
using Ksiegarnia.Domain;
using System.IdentityModel.Tokens.Jwt;

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

        public UserDTO Get(string login)
        {
            var user = userRepository.GetUser(login);
            if(user == null)
            {
                throw new Exception($"user with login: '{login}' does't exist.");
            }
            var userDto = new UserDTO()
            {
                UserId = user.UserId,
                Login = user.Login,
                Email = user.Email,
                CreatedAt = user.CreatedAt,
                Orders = user.Orders
            };

            return userDto;
        }

        public AuthenticationResult Login(string login, string password)
        {
            var user = userRepository.GetUser(login);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            string salt = user.Salt;
            string hash = encrypter.GetHash(password, salt);
            if (user.Password != hash)
            {
                throw new Exception("Invalid credentials");
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

            loggedUserRepository.AddLoggedUser(refreshToken);
            loggedUserRepository.SaveChanges();

            authResult.RefreshToken = refreshToken.RefreshToken;
            return authResult;
        }

        public AuthenticationResult RefreshConnection(string jwtToken, string refreshToken)
        {
            var loggedUser = loggedUserRepository.GetLoggedUser(Guid.Parse(refreshToken));

            var authResult = jwtService.RefreshToken(jwtToken, loggedUser);

            loggedUser.JwtId = authResult.JwtId;
            loggedUserRepository.UpdateLoggedUser(loggedUser);
            loggedUserRepository.SaveChanges();
            authResult.RefreshToken = loggedUser.RefreshToken;

            return authResult;
        }

        public void Logout()
        {
            var jwt = jwtService.GetCurrentToken();
            var validatedToken = jwtService.GetPrincipalFromToken(jwt);
            //zbadaj czemu Authorize nie dziala na przedawnione tokeny
            var login = validatedToken.Claims.Single(x => x.Type == "login").Value;
            var userId = userRepository.GetUser(login).UserId;
            var JwtId = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            loggedUserRepository.RemoveLoggedUser(JwtId);
            loggedUserRepository.SaveChanges();

            jwtService.DeactivateCurrentToken();
        }

        public void Register(string login, string password, string email, AddressDTO addressDto = null)
        {
            var user = userRepository.GetUser(login);
            if (user != null)
            {
                throw new Exception($"User with login: '{login}' already exist.");
            }

            user = userRepository.GetUserByEmail(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist.");
            }

            string salt = encrypter.GetSalt();
            string hash = encrypter.GetHash(password, salt);
            user = new User(login, email, hash, salt);

            if (addressDto != null)
            {
                Address address = new Address(
                        user.UserId,
                        addressDto.City,
                        addressDto.Street,
                        addressDto.HouseNumber,
                        addressDto.ZipCode,
                        addressDto.FlatNumber
                    );
                userRepository.AddAddress(address);
            }
            userRepository.AddUser(user);
            userRepository.SaveChanges();
        }
    }
}
