namespace Ksiegarnia.Commands
{
    public class RefreshConnectionCommand
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}