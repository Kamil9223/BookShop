using Core.IRepositories;
using Core.Models;
using Infrastructure.Exceptions;
using Infrastructure.IServices;
using Infrastructure.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Testy_Jednostkowe.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task Register_method_should_invoke_GetUser_method_in_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggedUserRepositoryMock = new Mock<ILoggedUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();

            var userService = new UserService(userRepositoryMock.Object, loggedUserRepositoryMock.Object, 
                encrypterMock.Object, jwtService.Object);

            await userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Register_method_should_invoke_AddUser_method_in_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggedUserRepositoryMock = new Mock<ILoggedUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();

            var userService = new UserService(userRepositoryMock.Object, loggedUserRepositoryMock.Object,
                encrypterMock.Object, jwtService.Object);

            await userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task Register_method_should_throw_exception_when_user_already_exist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggedUserRepositoryMock = new Mock<ILoggedUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();

            var userService = new UserService(userRepositoryMock.Object, loggedUserRepositoryMock.Object,
                encrypterMock.Object, jwtService.Object);

            var user = new User("Kamil", "wrongMail", "pass", "salt");
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Task.FromResult(user));

            Func<Task> register = async () => await userService.Register("Kamil", "secret", "wrongMail");

            await Assert.ThrowsAsync<AlreadyExistException>(register);
        }

        [Fact]
        public async Task Login_method_should_throw_exception_when_given_password_is_not_equal_to_hash()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var loggedUserRepositoryMock = new Mock<ILoggedUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();

            var userService = new UserService(userRepositoryMock.Object, loggedUserRepositoryMock.Object,
                encrypterMock.Object, jwtService.Object);

            string correctPassword = "correct";
            string testPass = "fakePass";

            Encrypter encrypter = new Encrypter();
            string salt = encrypter.GetSalt();

            string hash = encrypter.GetHash(correctPassword, salt);
            var hashForTest = encrypter.GetHash(testPass, salt);
            var user = new User("Kamil", "email", hash, salt);       
            
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Task.FromResult(user));
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), user.Salt))
                                      .Returns(hashForTest);
                         

            Func<Task> login = async () => await userService.Login("testLogin", testPass);
            await Assert.ThrowsAsync<InvalidCredentialsException>(login);
        }
    }
}
