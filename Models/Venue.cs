using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace CLDV6211_Part1.Models
{
    public class Venue
    {
        public int VenueId { get; set; }
        public string? VenueName { get; set; }
        public string? Location { get; set; }
        public int Capacity { get; set; }
        public string? ImageUrl { get; set; }

        // Navigation properties
        public ICollection<Event>? Events { get; set; }
        public ICollection<Booking>? Bookings { get; set; }
    }
}
