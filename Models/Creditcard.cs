using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class Creditcard
    {
        public Creditcard()
        {
            Creditcardusers = new HashSet<Creditcarduser>();
        }

        public int CreditCardId { get; set; }
        public int CardNumber { get; set; }
        public int CardPin { get; set; }
        public string CardDate { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<Creditcarduser> Creditcardusers { get; set; }
    }
}
