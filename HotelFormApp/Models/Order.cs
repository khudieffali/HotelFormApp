using System;
using System.Collections.Generic;

#nullable disable

namespace HotelFormApp.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? RoomId { get; set; }
        public int? CustomerId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public bool? IsRefuned { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
