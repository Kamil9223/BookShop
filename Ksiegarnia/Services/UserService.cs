using Ksiegarnia.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ksiegarnia.DTO;
using Ksiegarnia.IRepositories;
using Ksiegarnia.Models;

namespace Ksiegarnia.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IJwtService jwtService;

        public UserService(IUserRepository userRepository, IEncrypter encrypter, IJwtService jwtService)
        {
            this.userRepository = userRepository;
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

        public void Login(string login, string password)
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
            
            var jwtToken = jwtService.CreateToken(user.Login, "user");
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
