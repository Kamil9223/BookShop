using AuthService.ApiContracts.Requests;
using AuthService.ApiContracts.Responses;
using DatabaseAccess.MSSQL_BookShop;
using IntegrationTests.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Controllers
{
    public class AuthHelper
    {
        private readonly HttpClient httpClient;
        private readonly BookShopContext dbContext;

        public AuthHelper(HttpClient httpClient, BookShopContext dbContext)
        {
            this.httpClient = httpClient;
            this.dbContext = dbContext;
        }

        public async Task<AuthenticationResponse> Authenticate()
        {
            var loginRequest = new LoginRequest
            {//TODO Globalnie wskazać jaki użytkownik jest testowo dodany do bazy
                Login = "testLogin",
                Password = "secretPass"
            };

            HttpContent httpContent = new StringContent(
                JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync($"/api/login", httpContent);
            var authResponse = await response.Content.ReadAsAsync<AuthenticationResponse>();

            httpClient.DefaultRequestHeaders.Add("Authorization", $"bearer {authResponse.JwtToken}");

            return authResponse;
        }
    }
}
