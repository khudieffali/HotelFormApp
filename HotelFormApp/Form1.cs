using HotelFormApp.Models;
using Microsoft.EntityFrameworkCore;
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
    public partial class Form1 : Form
    {
        HotelDBContext db = new();
        RoomType selectedType;
        User selectedUser;
        Order selectedOrder;
        Customer selectedCustom;
        Floor selectedFloor;
        Room selectedRoom;
        public Form1(User us)
        {
            selectedUser = us;
            InitializeComponent();
        }
        
        private void FillComboTypes()
        {
            cmbTypes.Items.AddRange(db.RoomTypes.Select(x => x.Name).ToArray());

        }
        private void FillComboNumber()
        {
            cmbRoomNumber.Items.AddRange(db.Rooms.Select(x => x.RoomNumber.ToString()).ToArray());
        }
        private void FillComboLevel()
        {
            cmbLevel.Items.AddRange(db.Floors.Select(x => x.FloorNumber.ToString()).ToArray());
        }
        private void ClearTxt(string txt)
        {
            if (txt == "a")
            {
                txtFullName.Text = "";
                txtPhone.Text = "";
                txtEmail.Text = "";
                txtIdentification.Text = "";
                dtCheckDate.Value = DateTime.Now;
                 dtCheckIn.Value = DateTime.Now;
                cmbTypes.Text = "";
                cmbLevel.Text = "";
                cmbRoomNumber.Text = "";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            FillComboTypes();
            FillComboNumber();
            FillGridOrders();
            FillComboLevel();
            lblUser.Text = "Welcome " + selectedUser.FullName;
            dtgOrders.RowsDefaultCellStyle.ForeColor = Color.Black;
        }
        private void FillGridOrders()
        {
            dtgOrders.DataSource = db.Orders.Where(x=>x.User.FullName==selectedUser.FullName && x.IsRefuned==false)
                .Select(x => new
            {
                    x.OrderId,
                AdSoyad= x.Customer.FullName,
                Telefon=x.Customer.PhoneNumber,
                x.Customer.Email,
                OtaginTipi=x.Room.Type.Name,
                OtagNomresi=x.Room.RoomNumber,
                OtagMertebesi=x.Room.Floor.FloorNumber,
                SifarisTarixi=x.CheckIn,
                BitmeTarixi=x.CheckDate,
                OtagQiymeti=x.TotalPrice,

            }).ToList();
            dtgOrders.Columns[0].Visible = false;
        }


        private void btnSell_Click(object sender, EventArgs e)
        {
            string FullName = txtFullName.Text;
            string Email = txtEmail.Text;
            string Phone = txtPhone.Text;
            string IdentificationId = txtIdentification.Text;
            string Types = cmbTypes.Text;
            string RoomNumber = cmbRoomNumber.Text;
            string Level = cmbLevel.Text;
            DateTime CheckDate = dtCheckDate.Value;
            DateTime CheckIn = dtCheckIn.Value;
            selectedType = db.RoomTypes.FirstOrDefault(x => x.Name == cmbTypes.Text);
            string[] arr = new string[] { FullName, Email, Phone, IdentificationId};
            if (Utilities.IsEmpty(arr))
            {
                if (dtCheckDate.Value >= dtCheckIn.Value)
                {
                    Customer newCustom = new()
                    {
                        FullName = FullName,
                        Email = Email,
                        PhoneNumber = Phone,
                        IdentificationId = Convert.ToInt32(IdentificationId),
                    };
                    db.Customers.Add(newCustom);
                    db.SaveChanges();
                    int RoomId = db.Rooms.FirstOrDefault(x => x.RoomNumber.ToString() == cmbRoomNumber.Text).RoomId;
                    int DateDay = ((TimeSpan)(CheckDate - CheckIn)).Days;
                    int UserId = db.Users.FirstOrDefault(x => x.UserName == selectedUser.UserName).UserId;
                    Order newOrder = new()
                    {
                        RoomId = RoomId,
                        CustomerId = newCustom.CustomerId,
                        CheckIn = CheckIn,
                        CheckDate = CheckDate,
                        TotalPrice = DateDay*selectedType.Price,
                        UserId = UserId,
                        IsRefuned = false

                    };

                    db.Orders.Add(newOrder);
                    db.SaveChanges();

                    MessageBox.Show("Sifaris Ugurla Elave Edildi", "success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearTxt("a");
                    FillGridOrders();
                    FillComboNumber();
                    FillComboLevel();
                }


            
            else
            {
                MessageBox.Show("Teqvimi duzgun daxil edin");
            }


            }
            else
            {
                MessageBox.Show("Xanalari Doldurun");
            }
        }

        private void cmbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            string TypeName = cmbTypes.Text;
            int TypeId = db.RoomTypes.FirstOrDefault(x => x.Name == TypeName).RoomTypeId;
            cmbRoomNumber.Items.Clear();
            cmbRoomNumber.Items.AddRange(db.Rooms.Where(x=>x.TypeId==TypeId).Select(x=>x.RoomNumber.ToString()).ToArray());
            cmbRoomNumber.Text = ""; 
        }

 
        private void cmbRoomNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string RoomNumber = cmbRoomNumber.Text;
            int NumberId = db.Rooms.FirstOrDefault(x => x.RoomNumber.ToString() == RoomNumber).RoomId;
            cmbLevel.Items.Clear();
            cmbLevel.Items.AddRange(db.Rooms.Where(x => x.RoomId == NumberId).Select(x => x.Floor.FloorNumber.ToString()).ToArray());
            cmbLevel.Text = "";
        }

        private void dtgOrders_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int orderId =(int) dtgOrders.Rows[e.RowIndex].Cells[0].Value;
            selectedOrder = db.Orders
                .Include(x=>x.Customer)
                .Include(x=>x.Room)
                .Include(x=>x.Room.Type)
                .Include(x=>x.Room.Floor)
                .FirstOrDefault(x => x.OrderId == orderId);
            selectedCustom = db.Customers.FirstOrDefault(x => x.FullName == selectedOrder.Customer.FullName);
            selectedType = db.RoomTypes.FirstOrDefault(x => x.Name == selectedOrder.Room.Type.Name);
            selectedRoom = db.Rooms.FirstOrDefault(x => x.RoomNumber == selectedOrder.Room.RoomNumber);
            selectedFloor = db.Floors.FirstOrDefault(x => x.FloorNumber == selectedOrder.Room.Floor.FloorNumber);
            txtFullName.Text = selectedCustom.FullName;
            txtEmail.Text = selectedCustom.Email;
            txtPhone.Text = selectedCustom.PhoneNumber;
            txtIdentification.Text = selectedCustom.IdentificationId.ToString();
            cmbTypes.Text = selectedType.Name;
            cmbRoomNumber.Text = selectedRoom.RoomNumber.ToString();
            cmbLevel.Text = selectedRoom.Floor.FloorNumber.ToString();
            dtCheckIn.Value = selectedOrder.CheckIn.Value;
            dtCheckDate.Value = selectedOrder.CheckDate.Value;
            EnableBtn("a");
        }

        private void EnableBtn(string text)
        {
            if (text == "a")
            {
                btnSell.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnSell.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string phoneNumber = txtPhone.Text;
            string email = txtEmail.Text;
            string identification = txtIdentification.Text;
            string roomType = cmbTypes.Text;
            string roomNumber = cmbRoomNumber.Text;
            string level = cmbLevel.Text;
            DateTime checkIn = dtCheckIn.Value;
            DateTime checkDate = dtCheckDate.Value;
            fullName = selectedOrder.Customer.FullName;

            selectedOrder.Customer.Email = email ;
            selectedOrder.Customer.PhoneNumber=phoneNumber;
            selectedOrder.Customer.IdentificationId=Convert.ToInt32( identification);
            selectedOrder.Room.Type.Name=roomType;
            selectedOrder.Room.RoomNumber=Convert.ToInt32(roomNumber);
            selectedOrder.Room.Floor.FloorNumber = Convert.ToInt32(level) ;
            selectedOrder.CheckIn=checkIn;
            selectedOrder.CheckDate=checkDate;
            db.SaveChanges();
            MessageBox.Show("The change was successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearTxt("a");
            FillGridOrders();
            EnableBtn("b");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure to delete this?", "Deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                selectedOrder.IsRefuned = true;
                db.SaveChanges();
                ClearTxt("a");
                FillGridOrders();
                EnableBtn("b");
            }
        }
    }
}
