using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbRoomCategory
    {
        public TbRoomCategory()
        {
            TbRooms = new HashSet<TbRoom>();
            Features = new HashSet<TbCategoryFeature>();
        }

        public int RoomCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Cost { get; set; }
        public string Description { get; set; } = null!;
        public int ImageId { get; set; }
        public int? OfferId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TbOffer? Offer { get; set; }
        public virtual ICollection<TbRoom> TbRooms { get; set; }

        public virtual ICollection<TbCategoryFeature> Features { get; set; }
    }
}
