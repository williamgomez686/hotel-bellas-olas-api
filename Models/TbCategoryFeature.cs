using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbCategoryFeature
    {
        public TbCategoryFeature()
        {
            Categories = new HashSet<TbRoomCategory>();
        }

        public int FeatureId { get; set; }
        public string Type { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbRoomCategory> Categories { get; set; }
    }
}
