using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class LoggedUser
    {
        public Guid RefreshToken { get; set; }
        public string JwtId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public virtual Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
