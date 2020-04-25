using Ksiegarnia.IServices;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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
            var token = jwtService.GetCurrentToken();

            if (String.IsNullOrEmpty(token))
            {
                await next(context);
                return;
            }

            var validatedToken = jwtService.GetPrincipalFromToken(token);

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTImeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

            if (await jwtService.IsCurrentActive() && expiryDateTImeUtc > DateTime.UtcNow)
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
