using System;
using System.Collections.Generic;

namespace hotel_bellas_olas_api.Models
{
    public partial class TbRole
    {
        public TbRole()
        {
            TbUsers = new HashSet<TbUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool? IsDeleted { get; set; }

        public virtual ICollection<TbUser> TbUsers { get; set; }
    }
}
