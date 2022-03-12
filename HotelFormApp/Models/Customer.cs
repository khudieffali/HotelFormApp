using System;
using System.Collections.Generic;

#nullable disable

namespace HotelFormApp.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int IdentificationId { get; set; }
       

        public virtual ICollection<Order> Orders { get; set; }
    }
}
