using System;
using System.Collections.Generic;

namespace Core.Models
{
    public class User
    {
        public Guid UserId { get; protected set; }
        public string Login { get; protected set; }
        public string Email { get; protected set; }
        public string Password { get; protected set; }
        public string Salt { get; protected set; }
        public UserRole Role { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public virtual ICollection<Order> Orders { get; protected set; }

        protected User() { }

        public User(string login, string email, string password, string salt)
        {
            UserId = Guid.NewGuid();
            Login = login;
            Email = email.ToLowerInvariant();
            Password = password;
            Salt = salt;
            CreatedAt = DateTime.Now;
            Role = UserRole.User;
        }
    }

    public enum UserRole
    {
        User,
        Admin
    }
}
