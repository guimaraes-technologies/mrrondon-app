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
        public IList<Contact> Contacts { get; set; }
    }
}