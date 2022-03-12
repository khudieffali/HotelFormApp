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
    public partial class CustomerViewForm : Form
    {
        HotelDBContext db = new();
        public CustomerViewForm()
        {
            InitializeComponent();
        }
        private void FillCustomerGrid()
        {
            dtgCustomers.DataSource = db.Customers.Select(x => new
            {
                x.FullName,
                x.PhoneNumber,
                x.Email,
                x.IdentificationId,
            }).ToList();
        }
        private void CustomerViewForm_Load(object sender, EventArgs e)
        {
            FillCustomerGrid();
        }
    }
}
