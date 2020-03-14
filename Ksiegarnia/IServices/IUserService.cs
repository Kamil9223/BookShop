using Ksiegarnia.Domain;
using Ksiegarnia.Requests;
using Ksiegarnia.Responses;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IUserService
    {
        Task Register(string login, string password, string email, AddressRequest addressDto = null);
        Task<AuthenticationResult> Login(string login, string password);
        Task<AuthenticationResult> RefreshConnection(string jwtToken, string refreshToken);
        Task Logout();
        Task<UserResponse> Get(string login);
    }
}
