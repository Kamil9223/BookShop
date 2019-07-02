using Ksiegarnia.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    public interface IUserService
    {
        void Register(string login, string password, string email, AddressDTO addressDto = null);
        void Login(string login, string password);
        void Logout(string jwtToken);
        UserDTO Get(string login);
    }
}
