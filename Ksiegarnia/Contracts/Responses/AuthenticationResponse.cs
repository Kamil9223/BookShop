namespace Ksiegarnia.Contracts.Responses
{
    public class AuthenticationResponse
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
