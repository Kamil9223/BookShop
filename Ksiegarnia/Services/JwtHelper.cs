using Ksiegarnia.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ksiegarnia.Services
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
