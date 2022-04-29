using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Hotelfeature
    {
        public int FeatureId { get; set; }
        public int ImageId { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
