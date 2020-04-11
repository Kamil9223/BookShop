using Ksiegarnia.Contracts.Requests;
using Ksiegarnia.DB;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestyIntegracyjne.Helpers;
using Xunit;

namespace TestyIntegracyjne.Controllers
{
    public class UserControllerTest : IntegralTestConfiguration
    {
        public BookShopContext dbContext;

        public UserControllerTest()
        {
            SeedDatabase();
        }

        protected override void SeedDatabase()
        {
            dbContext = TestServer.Host.Services.GetService(typeof(BookShopContext)) as BookShopContext;
            dbContext.Users.Add(new User("testLogin", "test@ll.com","passHash", "sampleSalt"));
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task GetUser_TryGetExistingUser_ShouldReturnUserFromDb()
        {
            var defaultUserLogin = "testLogin";

            var response = await HttpClient.GetAsync($"/api/user/{defaultUserLogin}/get");
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
            var response = await HttpClient.PostAsync($"/api/register", httpContent);

            response.EnsureSuccessStatusCode();

            var dbUserName = dbContext.Users.SingleOrDefault(x => x.Login == registerRequest.Login);

            Assert.NotNull(dbUserName);
            Assert.Equal(dbUserName.Email, registerRequest.Email, true);
        }
    }
}
