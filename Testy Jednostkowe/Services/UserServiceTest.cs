﻿using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using Ksiegarnia.Models;
using Ksiegarnia.Services;
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

            Action register = async () => await userService.Register("Kamil", "secret", "wrongMail");

            Assert.Throws<Exception>(register);
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

            //correct password for login=Kamil is kamil.
            string testPass = "fakePass";
            var user = new User("Kamil", "email", "K7UbVVzydbxhowe4PBq2yEcdgJmaSqk65w1uymlcZcTIzcfLlf2hztqfFiN6ii2pPKg=", "gdNA8qsL9sgD/ZVjtt9ZMjbNDmUgJf4WVQPVYkkO7UaKVlIlzI/5JiJYG2ZdeGQvT6s=");
            Encrypter encrypter = new Encrypter();
            var hashForTest = encrypter.GetHash(testPass, user.Salt);

            userRepositoryMock.Setup(x => x.GetUser(It.IsAny<string>())).Returns(Task.FromResult(user));
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), user.Salt))
                                      .Returns(hashForTest);
                         

            Action login = async () => await userService.Login("Kamil", testPass);
            Assert.Throws<Exception>(login);
        }
    }
}
