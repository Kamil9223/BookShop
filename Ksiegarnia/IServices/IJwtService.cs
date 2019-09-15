using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IJwtService
    {
        string CreateToken(string login, string role);
        Task DeactivateToken(string token);
        Task DeactivateCurrentToken();
        Task<bool> IsActive(string token);
        Task<bool> IsCurrentActive();
    }
}
