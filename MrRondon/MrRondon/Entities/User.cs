using System;

namespace MrRondon.Entities
{
    public class User
    {
        public Guid UserId { get; set; }

        public Guid PersonId { get; set; }
        public Person Person { get; set; }
    }
}