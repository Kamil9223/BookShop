﻿using AuthService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.MiddleWares
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

            if (string.IsNullOrEmpty(token))
            {
                await next(context);
                return;
            }

            var validatedToken = jwtService.GetPrincipalFromToken(token);

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTIme = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(expiryDateUnix).ToLocalTime();

            if (await jwtService.IsCurrentActive() && expiryDateTIme > DateTime.Now)
            {
                await next(context);
                return;
            }

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
    }
}
