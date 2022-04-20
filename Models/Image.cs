using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Image
    {
        public Image()
        {
            Advertisings = new HashSet<Advertising>();
        }

        public int ImageId { get; set; }
        public string Name { get; set; } = null!;
        public string? Content { get; set; }
        public int CategoryId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Imagecatalog Category { get; set; } = null!;
        public virtual ICollection<Advertising> Advertisings { get; set; }
    }
}
