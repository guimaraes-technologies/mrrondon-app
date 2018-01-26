using System;

namespace MrRondon.Entities
{
    public class Event
    {
        public Guid EventId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public string RangeDateAndValue => $"Início: {StartDate.ToShortDateString()}\nTérminio: {EndDate.ToShortDateString()}\nValor: {Value:C}";
    }
}