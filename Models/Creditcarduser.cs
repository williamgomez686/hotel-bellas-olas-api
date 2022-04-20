using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Creditcarduser
    {
        public int CreditCardId { get; set; }
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Creditcard CreditCard { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
