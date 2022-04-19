using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbRoom
    {
        public TbRoom()
        {
            TbReservations = new HashSet<TbReservation>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; } = null!;
        public int RoomCategoryId { get; set; }
        public int RoomNumber { get; set; }
        public bool Status { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TbRoomCategory RoomCategory { get; set; } = null!;
        public virtual ICollection<TbReservation> TbReservations { get; set; }
    }
}
