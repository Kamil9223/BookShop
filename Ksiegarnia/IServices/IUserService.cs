using Ksiegarnia.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.IServices
{
    interface IUserService
    {
        void Register(string login, string password, string email, AddressDTO addressDto);
        UserDTO Get(string login);
    }
}
