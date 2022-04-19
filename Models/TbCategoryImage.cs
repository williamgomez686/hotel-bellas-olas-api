using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbCategoryImage
    {
        public TbCategoryImage()
        {
            Images = new HashSet<TbImage>();
        }

        public int ImageCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbImage> Images { get; set; }
    }
}
