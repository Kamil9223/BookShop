using Ksiegarnia;
using Ksiegarnia.DB;
using Ksiegarnia.Models;
using Ksiegarnia.Responses;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
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
            dbContext.Users.Add(new User("testLogin", "test@ll.com",
                "passHash", "sampleSalt"));
            dbContext.SaveChanges();
        }

        //docelowo extension do COntent
        private T ReadAsAsync<T>(string content)
        {
            return JsonConvert.DeserializeObject<T>(content);
        }

        [Fact]
        public async Task GetUser_TryGetExistingUser_ShouldReturnUserFromDb()
        {
            var defaultUserLogin = "testLogin";

            var response = await HttpClient.GetAsync($"/api/user/{defaultUserLogin}/get");
            var user = ReadAsAsync<UserResponse>(await response.Content.ReadAsStringAsync());

            response.EnsureSuccessStatusCode();
            Assert.Equal("testLogin", user.Login);
            Assert.Equal("test@ll.com", user.Email);
        }
    }
}
