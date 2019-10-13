using Ksiegarnia.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Commands
{
    public class RegisterCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
    }
}
