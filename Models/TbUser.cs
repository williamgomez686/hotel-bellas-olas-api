using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbUser
    {
        public TbUser()
        {
            TbCreditCardUsers = new HashSet<TbCreditCardUser>();
            TbReservations = new HashSet<TbReservation>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual TbRole Role { get; set; } = null!;
        public virtual ICollection<TbCreditCardUser> TbCreditCardUsers { get; set; }
        public virtual ICollection<TbReservation> TbReservations { get; set; }
    }
}
