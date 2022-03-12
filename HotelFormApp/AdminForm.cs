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
 
    public partial class AdminForm : Form
    {
        HotelDBContext db = new();
      
        public AdminForm()
        {
           
            InitializeComponent();
        }

        
        private void pckReservation_Click(object sender, EventArgs e)
        {
            ReservationList rl = new();
            rl.ShowDialog();
        }

        private void pckUser_Click(object sender, EventArgs e)
        {
            RegisterForm rg = new();
            rg.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            CustomerViewForm cvf = new();
            cvf.ShowDialog();
        }
    }
}
