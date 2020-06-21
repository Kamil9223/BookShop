using Infrastructure.IServices;
using System.Security.Claims;

namespace Infrastructure.Services
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IJwtService jwtService;

        public JwtHelper(IJwtService jwtService)
        {
            this.jwtService = jwtService;
        }

        public ClaimsPrincipal GetClaimsFromToken()
        {
            var token = jwtService.GetCurrentToken();
            var validatedToken = jwtService.GetPrincipalFromToken(token);

            return validatedToken;
        }
    }
}
