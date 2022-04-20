using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Reservation
    {
        public int ReservationId { get; set; }
        public string? ReservationNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Room Room { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
