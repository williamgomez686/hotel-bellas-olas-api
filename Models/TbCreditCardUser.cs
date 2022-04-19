using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbCreditCardUser
    {
        public int CreditCardId { get; set; }
        public int UserId { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual TbCreditCard CreditCard { get; set; } = null!;
        public virtual TbUser User { get; set; } = null!;
    }
}
