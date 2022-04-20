using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Roomcategory
    {
        public Roomcategory()
        {
            Rooms = new HashSet<Room>();
            Features = new HashSet<Featurecatalog>();
        }

        public int RoomCategoryId { get; set; }
        public string Name { get; set; } = null!;
        public int Cost { get; set; }
        public string Description { get; set; } = null!;
        public int ImageId { get; set; }
        public int? OfferId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Offer? Offer { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

        public virtual ICollection<Featurecatalog> Features { get; set; }
    }
}
