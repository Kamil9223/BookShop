using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public virtual IEnumerable<Order> Orders { get; protected set; }

        protected User() { }

        public User(string login, string email, string password, string salt)
        {
            UserId = Guid.NewGuid();
            Login = login;
            Email = email.ToLowerInvariant();
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
