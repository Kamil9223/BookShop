using Ksiegarnia.IRepositories;
using Ksiegarnia.IServices;
using Ksiegarnia.Models;
using Ksiegarnia.Services;
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

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object);

            userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.GetUser(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Register_method_should_invoke_AddUser_method_in_repository()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();

            var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object);

            userService.Register("Test", "password", "wrongMail");

            userRepositoryMock.Verify(x => x.AddUser(It.IsAny<User>()), Times.Once);
        }

        //[Fact]
        //public void Register_method_should_throw_exception_when_user_already_exist()
        //{
        //    var userRepositoryMock = new Mock<IUserRepository>();
        //    var encrypterMock = new Mock<IEncrypter>();

        //    var userService = new UserService(userRepositoryMock.Object, encrypterMock.Object);

        //    Action register = () => userService.Register("Jan", "secret", "wrongMail");

        //    Assert.Throws<Exception>(register); 
        //}
    }
}
