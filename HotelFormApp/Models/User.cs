using System;
using System.Collections.Generic;

#nullable disable

namespace HotelFormApp.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            RoleToUsers = new HashSet<RoleToUser>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<RoleToUser> RoleToUsers { get; set; }
    }
}
