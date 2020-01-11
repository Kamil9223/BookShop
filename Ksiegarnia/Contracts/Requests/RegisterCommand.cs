using Ksiegarnia.DTO;

namespace Ksiegarnia.Contracts.Requests
{
    public class RegisterRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public AddressDTO Address { get; set; }
    }
}
