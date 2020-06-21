namespace Infrastructure.Contracts.Requests
{
    public class RefreshConnectionRequest
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}