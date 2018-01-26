using System;
using MrRondon.Converters;
using Newtonsoft.Json;

namespace MrRondon.Entities
{
    public class Address
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string ZipCode { get; set; }
        //[JsonConverter(typeof(DoubleJsonConverter))]
        public double Latitude { get; set; }
        //[JsonConverter(typeof(DoubleJsonConverter))]
        public double Longitude { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string FullAddressInline
        {
            get
            {
                var address = $"{Street}, {Number} - {Neighborhood}";
                return City == null ? address : $"{address}, {City.Name} - RO, {ZipCode}";
            }
        }

        public string FullAddress
        {
            get
            {
                var address = $"{Street}, {Number}\n{Neighborhood}";
                return City == null ? address : $"{address}\n{City.Name} - RO\n{ZipCode}";
            }
        }
    }
}