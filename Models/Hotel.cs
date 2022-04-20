using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Hotel
    {
        public int HotelId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? AboutUs { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
