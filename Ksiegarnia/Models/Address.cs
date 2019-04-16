using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    public class Address
    {
        public Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public string HouseNumber { get; protected set; }
        public string FlatNumber { get; protected set; }
        public string ZipCode { get; protected set; }

        protected Address() { }

        public Address(Guid userId, string city, string street, string houseNumber, string zipCode)
        {
            UserId = userId;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }
    }
}
