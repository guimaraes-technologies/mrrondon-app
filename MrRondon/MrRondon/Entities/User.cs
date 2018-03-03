using System;
using System.Collections.Generic;

namespace MrRondon.Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Cpf { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CellPhone { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime CreateOn { get; set; }
        public IList<Contact> Contacts { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}