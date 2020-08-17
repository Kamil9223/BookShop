using System;

namespace AuthService.DTO
{
    public class AuthenticationResult
    {
        public string JwtToken { get; set; }
        public string JwtId { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
