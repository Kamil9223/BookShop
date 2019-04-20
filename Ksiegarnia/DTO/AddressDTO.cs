using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.DTO
{
    public class AddressDTO
    {
        public string City { get; protected set; }
        public string Street { get; protected set; }
        public string HouseNumber { get; protected set; }
        public string FlatNumber { get; protected set; }
        public string ZipCode { get; protected set; }
    }
}
