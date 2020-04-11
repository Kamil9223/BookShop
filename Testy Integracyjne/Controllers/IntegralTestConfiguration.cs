using Ksiegarnia;
using Ksiegarnia.DB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Net.Http;

namespace TestyIntegracyjne.Controllers
{
    public abstract class IntegralTestConfiguration
    {
        protected const string DbName = "TestDatabase";
        protected readonly HttpClient HttpClient;
        protected readonly TestServer TestServer;

        public IntegralTestConfiguration()
        {
            TestServer = new TestServer(new WebHostBuilder()
                .UseConfiguration(GetAppSettings())
                .UseStartup<Startup>()
                .ConfigureServices(services =>
                {
                    services.RemoveAll(typeof(BookShopContext));
                    services.AddDbContext<BookShopContext>(options => 
                    {
                        options.UseInMemoryDatabase(DbName);
                    });
                }));

            HttpClient = TestServer.CreateClient();
        }

        private IConfigurationRoot GetAppSettings()
        {
            var projectDir = AppDomain.CurrentDomain.BaseDirectory;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(projectDir)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            return configuration;
        }

        protected abstract void SeedDatabase();
    }
}
