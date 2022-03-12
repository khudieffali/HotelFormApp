using System;
using System.Collections.Generic;

#nullable disable

namespace HotelFormApp.Models
{
    public partial class Floor
    {
        public Floor()
        {
            Rooms = new HashSet<Room>();
        }

        public int FloorId { get; set; }
        public int FloorNumber { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
