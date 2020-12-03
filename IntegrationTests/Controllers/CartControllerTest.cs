using API.Requests.OrderRequests;
using AuthService.Services.Implementations;
using Core.Models;
using DatabaseAccess.MSSQL_BookShop;
using IntegrationTests.Helpers;
using Newtonsoft.Json;
using OrderService.DTO;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Controllers
{
    public class CartControllerTest : IntegralTestConfiguration
    {
        private BookShopContext dbContext;
        private Encrypter encrypter;

        private Guid BookId = Guid.NewGuid();
        private Guid CategoryId = Guid.NewGuid();

        public CartControllerTest()
        {
            encrypter = new Encrypter();
            SeedDatabase();
            AuthHelper = new AuthHelper(httpClient, dbContext);
            AuthHelper.Authenticate().Wait();
        }

        protected override void SeedDatabase()
        {
            dbContext = testServer.Host.Services.GetService(typeof(BookShopContext)) as BookShopContext;
            var salt = encrypter.GetSalt();
            var hash = encrypter.GetHash("secretPass", salt);
            dbContext.Users.Add(new User("testLogin", "test@ll.com", hash, salt));
            dbContext.Categories.Add(new Category(CategoryId, "Category"));
            dbContext.Books.Add(new Book(BookId, "testBook", 12.99M, 411, "short description", 13, CategoryId));
            dbContext.SaveChanges();
        }

        [Fact]
        public async Task ShowCart_should_returns_empty_cart()
        {
            var response = await httpClient.GetAsync($"/api/cart");

            var cart = await response.Content.ReadAsAsync<List<CartPosition>>();

            response.EnsureSuccessStatusCode();
            Assert.Empty(cart);
        }

        [Fact]
        public async Task AddCart_should_Create_product_in_session_cart()
        {
            var request = new AddToCartRequest { BookId = BookId };
            HttpContent httpContent = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json");

            var response = await httpClient.PostAsync($"/api/cart", httpContent);
            var content = await response.Content.ReadAsAsync<CreatedResponse>();

            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal("Resource added propperly.", content.Message);
        }
    }
}
