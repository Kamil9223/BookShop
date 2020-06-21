using Infrastructure.Contracts.Requests;
using Infrastructure.Contracts.Responses;
using Infrastructure.Domain.UserDomain;
using System.Threading.Tasks;

namespace Infrastructure.IServices
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
