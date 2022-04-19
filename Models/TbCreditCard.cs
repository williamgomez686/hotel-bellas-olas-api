using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbCreditCard
    {
        public TbCreditCard()
        {
            TbCreditCardUsers = new HashSet<TbCreditCardUser>();
        }

        public int CreditCardId { get; set; }
        public int CardNumber { get; set; }
        public int CardPin { get; set; }
        public string CardDate { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbCreditCardUser> TbCreditCardUsers { get; set; }
    }
}
