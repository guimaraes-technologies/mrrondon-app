using System;

namespace MrRondon.Entities
{
    public class FavoriteEvent
    {
        public Guid FavoriteEventId { get; set; }

        public Guid EventId { get; set; }
        public Event Event { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}