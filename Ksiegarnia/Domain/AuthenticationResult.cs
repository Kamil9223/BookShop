using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Domain
{
    public class AuthenticationResult
    {
        public string JwtToken { get; set; }
        public string JwtId { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
