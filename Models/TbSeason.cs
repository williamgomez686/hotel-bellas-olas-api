using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbSeason
    {
        public int SeasonId { get; set; }
        public string Type { get; set; } = null!;
        public int PercentApply { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
