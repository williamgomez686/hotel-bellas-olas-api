using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Offer
    {
        public Offer()
        {
            Roomcategories = new HashSet<Roomcategory>();
        }

        public int OfferId { get; set; }
        public int Name { get; set; }
        public string Description { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? OfferPercent { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Roomcategory> Roomcategories { get; set; }
    }
}
