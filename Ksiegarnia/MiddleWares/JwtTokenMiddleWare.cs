using Ksiegarnia.IServices;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace Ksiegarnia.MiddleWares
{
    public class JwtTokenMiddleWare : IMiddleware
    {
        private readonly IJwtService jwtService;

        public JwtTokenMiddleWare(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (await jwtService.IsCurrentActive())
            {
                await next(context);
                return;
            }
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
