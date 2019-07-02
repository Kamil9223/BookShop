using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IJwtService
    {
        string CreateToken(string login, string role);
        void DeleteToken(string jwtToken);
    }
}
