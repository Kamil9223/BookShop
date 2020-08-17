using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Models;
using AuthService.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace AuthService.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration config;
        private readonly IDistributedCache cache;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly TokenValidationParameters tokenValidationParameters;

        public JwtService(IConfiguration config, IDistributedCache cache, IHttpContextAccessor httpContextAccessor,
            TokenValidationParameters tokenValidationParameters)
        {
            this.config = config;
            this.cache = cache;
            this.httpContextAccessor = httpContextAccessor;
            this.tokenValidationParameters = tokenValidationParameters;
        }

        public AuthenticationResult CreateToken(string login, string role)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("login", login)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: config["Jwt:Issuer"],
                    claims: claims,
                    notBefore: DateTime.Now,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(config["Jwt:ExpiryMinutes"])),
                    signingCredentials: creds
                );

            var authResult = new AuthenticationResult
            {
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                JwtId = token.Id
            };

            return authResult;
        }

        public AuthenticationResult RefreshToken(string jwtToken, LoggedUser loggedUser)
        {
            var validatedToken = GetPrincipalFromToken(jwtToken);

            var expiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDateTIme = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(expiryDateUnix).ToLocalTime();

            if (expiryDateTIme > DateTime.Now)
            {
                throw new Exception("This JwtToken hasn't expire yet");
            }

            var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            if (loggedUser == null)
            {
                throw new Exception("This refesh token doesn't exist");
            }

            if (DateTime.Now > loggedUser.ExpiryDate)
            {
                throw new Exception("This refresh token has expired");
            }

            if (loggedUser.JwtId != jti)
            {
                throw new Exception("This refresh token doesn't match JWT");
            }

            var login = validatedToken.Claims.Single(x => x.Type == "login").Value;
            var authResult = CreateToken(login, "user");

            return authResult;
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenValidationParams = tokenValidationParameters.Clone();
            tokenValidationParams.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out var validatedToken);
            if (!IsJwtWithValidSecureAlgorithm(validatedToken))
                throw new Exception("Invalid Jwt Token");

            return principal;
        }

        private bool IsJwtWithValidSecureAlgorithm(SecurityToken validatedToken)
        {
            return validatedToken is JwtSecurityToken jwtValidatedSecurityToken &&
                jwtValidatedSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase);
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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(Convert.ToDouble(config["Jwt:ExpiryMinutes"] + 1))
            });
        }

        public async Task DeactivateCurrentToken()
        {
            await DeactivateToken(GetCurrentToken());
        }

        public string GetCurrentToken()
        {
            var authorizationHeader = httpContextAccessor.HttpContext.Request.Headers["authorization"];

            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(new[] { " " }, StringSplitOptions.None).Last();
        }
    }
}
