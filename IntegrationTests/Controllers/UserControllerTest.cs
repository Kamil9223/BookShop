using API.Requests.AuthRequests;
using AuthService.ApiContracts.Responses;
using Core.Models;
using DatabaseAccess.MSSQL_BookShop;
using Infrastructure.Services;
using IntegrationTests.Helpers;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class UserControllerTest : IntegralTestConfiguration
    {
        private BookShopContext dbContext;
        private Encrypter encrypter;

        public UserControllerTest()
        {
            encrypter = new Encrypter();
            SeedDatabase();
            AuthHelper = new AuthHelper(httpClient, dbContext);
        }

        protected override void SeedDatabase()
        {
            dbContext = testServer.Host.Services.GetService(typeof(BookShopContext)) as BookShopContext;
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash("secretPass", salt);
            dbContext.Users.Add(new User("testLogin", "test@ll.com", hash, salt));
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetUser_TryGetExistingUser_ShouldReturnUserFromDb()
        {
            var defaultUserLogin = "testLogin";

            var response = await httpClient.GetAsync($"/api/user/{defaultUserLogin}/get");
            var user = await response.Content.ReadAsAsync<UserResponse>();

            response.EnsureSuccessStatusCode();
            Assert.Equal("testLogin", user.Login);
            Assert.Equal("test@ll.com", user.Email);
        }

        [Fact]
        public async Task Register_CreateAndRegisterUserInApplication_NewUserShouldBeSavedInDb()
        {
            var registerRequest = new RegisterRequest
            {
                Login = "newTestLogin",
                Password = "secret",
                Email = "newTestLogin12@xd.com"
            };

            HttpContent httpContent = new StringContent(
                JsonConvert.SerializeObject(registerRequest),
                Encoding.UTF8,
                "application/json");
            var response = await httpClient.PostAsync($"/api/register", httpContent);

            response.EnsureSuccessStatusCode();

            var dbUserName = dbContext.Users.SingleOrDefault(x => x.Login == registerRequest.Login);

            Assert.NotNull(dbUserName);
            Assert.Equal(dbUserName.Email, registerRequest.Email, true);
        }

        [Fact]
        public async Task Login_LoginUsingExistingUserCredentials_MethodShouldReturnTokenAndSaveUserIntoDb()
        {
            var loginRequest = new LoginRequest
            {
                Login = "testLogin",
                Password = "secretPass"
            };

            HttpContent httpContent = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync($"/api/login", httpContent);

            response.EnsureSuccessStatusCode();
            var authResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();
            var loggedUser = dbContext.LoggedUsers.SingleOrDefault(
                x => x.RefreshToken == Guid.Parse(authResponse.RefreshToken));

            Assert.NotNull(loggedUser);
        }

        [Fact]
        public async Task Logout_LogoutAfterAuthentication_ShouldReturnSuccessStatusCode()
        {
            await AuthHelper.Authenticate();

            var response = await httpClient.PostAsync($"/api/logout", null);

            response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task Logout_LogoutWithoutAuthentication_ShouldReturnUnauthorized()
        {
            var response = await httpClient.PostAsync($"/api/logout", null);

            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task Refresh_LoginAndImmediatleyTryingRefreshToken_ShouldReturnInternalServerError()
        {
            var authResponse = await AuthHelper.Authenticate();
            var refreshRequest = new RefreshConnectionRequest
            {
                JwtToken = authResponse.JwtToken,
                RefreshToken = authResponse.RefreshToken
            };

            HttpContent httpContent = new StringContent(
                JsonConvert.SerializeObject(refreshRequest),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync($"/api/refresh", httpContent);

            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }
    }
}
