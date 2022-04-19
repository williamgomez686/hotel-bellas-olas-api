using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbOffer
    {
        public TbOffer()
        {
            TbRoomCategories = new HashSet<TbRoomCategory>();
        }

        public int OfferId { get; set; }
        public int Name { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? OfferPercent { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbRoomCategory> TbRoomCategories { get; set; }
    }
}
