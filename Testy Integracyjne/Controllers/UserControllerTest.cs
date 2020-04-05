using Ksiegarnia.DB;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using TestyIntegracyjne.Helpers;
using Xunit;

namespace TestyIntegracyjne.Controllers
{
    public class UserControllerTest : IntegralTestConfiguration
    {

        public UserControllerTest()
        {
            SeedDatabase();
        }

        protected override void SeedDatabase()
        {
            var dbContext = TestServer.Host.Services.GetService(typeof(BookShopContext)) as BookShopContext;
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
    }
}
