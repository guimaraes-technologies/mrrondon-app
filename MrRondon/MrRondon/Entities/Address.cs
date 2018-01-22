using System;

namespace MrRondon.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string FullAddress => $"{Street}, {Number} - {Neighborhood}, {ZipCode} - {City.Name}";
    }
}