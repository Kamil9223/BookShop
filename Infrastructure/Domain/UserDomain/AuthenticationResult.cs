using System;

namespace Infrastructure.Domain.UserDomain
{
    public class AuthenticationResult
    {
        public string JwtToken { get; set; }
        public string JwtId { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
