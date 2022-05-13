using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class User
    {
        public User()
        {
            Creditcardusers = new HashSet<Creditcarduser>();
            Reservations = new HashSet<Reservation>();
        }

        public int UserId { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int RoleId { get; set; }
        public string Email { get; set; } = null!;
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Creditcarduser> Creditcardusers { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
