using System.Security.Claims;

namespace AuthService.Services.Interfaces
{
    public interface IJwtHelper
    {
        ClaimsPrincipal GetClaimsFromToken();
    }
}
