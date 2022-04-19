using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbAdvertising
    {
        public int AdvertisingId { get; set; }
        public string? AdLink { get; set; }
        public int ImageId { get; set; }
        public string? Info { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TbImage Image { get; set; } = null!;
    }
}
