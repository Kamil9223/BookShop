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
        UserDTO Get(string login);
    }
}
