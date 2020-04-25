using Ksiegarnia.Domain.UserDomain;
using Ksiegarnia.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IJwtService
    {
        AuthenticationResult CreateToken(string login, string role);
        AuthenticationResult RefreshToken(string jwtToken, LoggedUser loggedUser);
        Task DeactivateToken(string token);
        Task DeactivateCurrentToken();
        Task<bool> IsActive(string token);
        Task<bool> IsCurrentActive();
        string GetCurrentToken();
        ClaimsPrincipal GetPrincipalFromToken(string token);
    }
}
