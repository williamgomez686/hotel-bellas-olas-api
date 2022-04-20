using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Imagecatalog
    {
        public Imagecatalog()
        {
            Images = new HashSet<Image>();
        }

        public int ImageCategoryId { get; set; }
        public string CatalogName { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Image> Images { get; set; }
    }
}
