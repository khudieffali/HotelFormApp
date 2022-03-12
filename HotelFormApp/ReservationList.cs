using HotelFormApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotelFormApp
{
    public partial class ReservationList : Form
    {
        HotelDBContext db = new();
        public ReservationList()
        {
            InitializeComponent();
        }
        private void FillGridOrders()
        {
            dtgReservation.DataSource = db.Orders
                .Select(x => new
                {
                    AdSoyad = x.Customer.FullName,
                    Telefon = x.Customer.PhoneNumber,
                    x.Customer.Email,
                    OtagNomresi = x.Room.RoomNumber,
                    OtagMertebesi = x.Room.Floor.FloorNumber,
                    SifarisTarixi = x.CheckIn,
                    BitmeTarixi = x.CheckDate,
                    OtagQiymeti = x.TotalPrice,

                }).ToList();

        }
        private void ReservationList_Load(object sender, EventArgs e)
        {
            FillGridOrders();
        }
    }
}
