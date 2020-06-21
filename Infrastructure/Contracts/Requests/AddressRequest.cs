﻿namespace Infrastructure.Contracts.Requests
{
    public class AddressRequest
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string FlatNumber { get; set; }
        public string ZipCode { get; set; }
    }
}