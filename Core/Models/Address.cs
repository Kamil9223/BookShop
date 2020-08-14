using System;

namespace Core.Models
{
    public class Address
    {
        public Guid AddressId { get; protected set; }
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public string HouseNumber { get; protected set; }
        public string FlatNumber { get; protected set; }
        public string ZipCode { get; protected set; }
        public virtual Guid UserId { get; protected set; }
        public virtual User User { get; protected set; }

        protected Address() { }

        public Address(Guid userId, string city, string street, string houseNumber, string zipCode, string flatNumber = null)
        {
            AddressId = Guid.NewGuid();
            UserId = userId;
            City = city;
            Street = street;
            HouseNumber = houseNumber;
            ZipCode = zipCode;
            FlatNumber = flatNumber;
        }
    }
}
