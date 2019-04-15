using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Models
{
    [Table("Addresses")]
    public class Address
    {
        [Key]
        public Guid AddressId { get; protected set; }
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public string HouseNumber { get; protected set; }
        public string FlatNumber { get; protected set; }
        public string ZipCode { get; protected set; }

        protected Address() { }

        public Address(string city, string street, string houseNumber, string zipCode)
        {
            AddressId = Guid.NewGuid();
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
        }
    }
}
