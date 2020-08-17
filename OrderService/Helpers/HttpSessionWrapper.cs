using Microsoft.AspNetCore.Http;

namespace OrderService.Helpers
{
    public class HttpSessionWrapper : IHttpSessionWrapper
    {
        private readonly ISession session;

        public HttpSessionWrapper(IHttpContextAccessor httpContextAccessor)
        {
            session = httpContextAccessor.HttpContext.Session;
        }

        public string GetString(string key)
        {
            return session.GetString(key);
        }

        public void SetString(string key, string value)
        {
            session.SetString(key, value);
        }
    }
}
