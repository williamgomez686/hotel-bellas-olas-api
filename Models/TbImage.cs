using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbImage
    {
        public TbImage()
        {
            TbAdvertisings = new HashSet<TbAdvertising>();
            Categories = new HashSet<TbCategoryImage>();
        }

        public int ImageId { get; set; }
        public string Name { get; set; } = null!;
        public string? Content { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbAdvertising> TbAdvertisings { get; set; }

        public virtual ICollection<TbCategoryImage> Categories { get; set; }
    }
}
