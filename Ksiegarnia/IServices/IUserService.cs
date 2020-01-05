using Ksiegarnia.Domain;
using Ksiegarnia.DTO;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IUserService
    {
        Task Register(string login, string password, string email, AddressDTO addressDto = null);
        Task<AuthenticationResult> Login(string login, string password);
        Task<AuthenticationResult> RefreshConnection(string jwtToken, string refreshToken);
        Task Logout();
        Task<UserDTO> Get(string login);
    }
}
