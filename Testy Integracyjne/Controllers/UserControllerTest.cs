using Ksiegarnia;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestyIntegracyjne.Configuration;
using Xunit;

namespace TestyIntegracyjne.Controllers
{
    public class UserControllerTest : Config
    {
        [Fact]
        public async Task test()
        {
            var response = await httpClient.GetAsync("api/User/Test");
            response.EnsureSuccessStatusCode();
        }
    }
}
