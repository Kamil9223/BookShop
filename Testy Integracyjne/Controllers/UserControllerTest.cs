using Ksiegarnia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace TestyIntegracyjne.Controllers
{
    public class UserControllerTest 
    {
        protected readonly HttpClient httpClient;
        private readonly TestServer testServer;

        public UserControllerTest()
        {
            testServer = new TestServer(new WebHostBuilder()
                     .UseStartup<Startup>());
            httpClient = testServer.CreateClient();
        }

        [Fact]
        public async Task test()
        {
            var response = await httpClient.GetAsync("/api/Books?page=1&pageSize=10");
            response.EnsureSuccessStatusCode();
        }
    }
}
