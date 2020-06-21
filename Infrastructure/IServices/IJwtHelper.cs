using System.Security.Claims;

namespace Infrastructure.IServices
{
    public interface IJwtHelper
    {
        ClaimsPrincipal GetClaimsFromToken();
    }
}
