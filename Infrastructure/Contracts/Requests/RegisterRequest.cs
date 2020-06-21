namespace Infrastructure.Contracts.Requests
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AddressRequest Address { get; set; }
    }
}
