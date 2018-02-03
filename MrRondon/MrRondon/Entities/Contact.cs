using System;

namespace MrRondon.Entities
{
   public class Contact
    {
        public Guid ContactId { get; set; }
        public string Description { get; set; }
        public ContactType ContactType { get; set; }

        public Guid? UserId { get; set; }
        public User User { get; set; }

        public Guid? CompanyId { get; set; }
        public Company Company { get; set; }
    }
}