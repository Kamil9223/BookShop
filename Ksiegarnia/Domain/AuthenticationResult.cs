using System;

namespace Ksiegarnia.Domain
{
    public class AuthenticationResult
    {
        public string JwtToken { get; set; }
        public string JwtId { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
