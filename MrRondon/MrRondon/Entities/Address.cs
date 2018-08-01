using System;
using System.Text;
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

        public string ParcialAddress => $"{Street}, {Number} - {Neighborhood}";

        public string FullAddress
        {
            get
            {
                var address = new StringBuilder();
                if (!string.IsNullOrWhiteSpace(Street)) address.Append($"{Street}, ");
                if (!string.IsNullOrWhiteSpace(Number)) address.Append($"{Number}\n");
                if (!string.IsNullOrWhiteSpace(Neighborhood)) address.Append($"{Neighborhood}");
                if (City == null) return address.ToString();

                address.Append($"\n{City.Name} - RO\n{ZipCode}");
                return address.ToString();
            }
        }
    }
}