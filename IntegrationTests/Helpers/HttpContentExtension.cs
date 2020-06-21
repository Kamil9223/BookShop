using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace IntegrationTests.Helpers
{
    public static class HttpContentExtension
    {
        public async static Task<T> ReadAsAsync<T>(this HttpContent httpContent)
        {
            var stringContent = await httpContent.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringContent);
        }
    }
}
