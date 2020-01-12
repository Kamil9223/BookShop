using Ksiegarnia.Models;
using System;
using System.Collections.Generic;

namespace Ksiegarnia.Responses
{
    public class UserResponse
    {
        public Guid UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}
