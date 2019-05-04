using Ksiegarnia;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace TestyIntegracyjne.Configuration
{
    public class Config
    {
        protected readonly HttpClient httpClient;
        private readonly TestServer testServer;

        public Config()
        {
            testServer = new TestServer(new WebHostBuilder()
                     .UseStartup<Startup>());
            httpClient = testServer.CreateClient();
        }
    }
}
