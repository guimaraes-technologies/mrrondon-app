using System;

namespace MrRondon.Entities
{
    public class Company
    {
        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public string FancyName { get; set; }

        public string Cnpj { get; set; }

        public int SegmentId { get; set; }//Category or subcategory
        public Category SubCategory { get; set; }

        public Guid AddressId { get; set; }
        public Address Address { get; set; }

        public string TelephoneAndEmail { get; set; }
    }
}