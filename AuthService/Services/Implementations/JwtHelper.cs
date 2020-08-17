using System.Security.Claims;
using AuthService.Services.Interfaces;

namespace AuthService.Services.Implementations
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
