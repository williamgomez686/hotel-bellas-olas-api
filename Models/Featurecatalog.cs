using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Featurecatalog
    {
        public Featurecatalog()
        {
            Categories = new HashSet<Roomcategory>();
        }

        public int FeatureId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Roomcategory> Categories { get; set; }
    }
}
