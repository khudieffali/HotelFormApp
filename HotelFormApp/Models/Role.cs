using System;
using System.Collections.Generic;

#nullable disable

namespace HotelFormApp.Models
{
    public partial class Role
    {
        public Role()
        {
            RoleToUsers = new HashSet<RoleToUser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<RoleToUser> RoleToUsers { get; set; }
    }
}
