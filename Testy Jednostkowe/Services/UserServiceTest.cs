using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using Ksiegarnia.Models;
using Ksiegarnia.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testy_Jednostkowe.Services
{
    public class UserServiceTest
    {
        [Fact]
        public void Register_method_should_invoke_GetUser_method_in_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();
            var memoryCache = new Mock<IMemoryCache>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
                jwtService.Object, memoryCache.Object);

            userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Register_method_should_invoke_AddUser_method_in_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();
            var memoryCache = new Mock<IMemoryCache>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
                jwtService.Object, memoryCache.Object);

            userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public void Register_method_should_throw_exception_when_user_already_exist()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();
            var memoryCache = new Mock<IMemoryCache>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
               jwtService.Object, memoryCache.Object);

            var user = new User("Kamil", "wrongMail", "pass", "salt");
            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);

            Action register = () => userService.Register("Kamil", "secret", "wrongMail");

            Assert.Throws<Exception>(register);
        }

        [Fact]
        public void Login_method_should_throw_exception_when_given_password_is_not_equal_to_hash()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var jwtService = new Mock<IJwtService>();
            var memoryCache = new Mock<IMemoryCache>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object,
               jwtService.Object, memoryCache.Object);

            //correct password for login=Kamil is kamil.
            string testPass = "fakePass";
            var user = new User("Kamil", "email", "K7UbVVzydbxhowe4PBq2yEcdgJmaSqk65w1uymlcZcTIzcfLlf2hztqfFiN6ii2pPKg=", "gdNA8qsL9sgD/ZVjtt9ZMjbNDmUgJf4WVQPVYkkO7UaKVlIlzI/5JiJYG2ZdeGQvT6s=");
            Encrypter encrypter = new Encrypter();
            var hashForTest = encrypter.GetHash(testPass, user.Salt);

            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(user);
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), user.Salt))
                                      .Returns(hashForTest);
                         

            Action login = () => userService.Login("Kamil", testPass);
            Assert.Throws<Exception>(login);
        }
    }
}
