using Ksiegarnia.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ksiegarnia.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;
        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;

        public JwtService(IConfiguration config, IDistributedCache cache, IHttpContextAccessor httpContextAccessor)
        {
            this.config = config;
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
        }

        public string CreateToken(string login, string role)
        {
            var claims = new[] 
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    claims: claims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(config["Jwt:ExpiryMinutes"])),
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<bool> IsActive(string token)
        {
            return await cache.GetStringAsync(token) == null;
        }

        public async Task<bool> IsCurrentActive()
        {
            return await IsActive(GetCurrentToken());
        }

        public async Task DeactivateToken(string token)
        {
            await cache.SetStringAsync(token, " ", new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(config["Jwt:ExpiryMinutes"]))
            });
        }

        public async Task DeactivateCurrentToken()
        {
            await DeactivateToken(GetCurrentToken());
        }

        private string GetCurrentToken()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? String.Empty
                : authorizationHeader.Single().Split(" ").Last();
        }
    }
}
