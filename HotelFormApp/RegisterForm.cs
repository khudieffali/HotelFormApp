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
    public partial class RegisterForm : Form
    {
        HotelDBContext db = new();
        User selectedUser;
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string fullName = txtFullName.Text;
            string password = txtPassword.Text;
            User selectedUser = db.Users.FirstOrDefault(x => x.UserName == userName);
            string[] myArr=new string[]{userName,fullName,password};
            if (Utilities.IsEmpty(myArr))
            {
                if (selectedUser == null)
                {
                    User newUser = new()
                    {
                        UserName = userName,
                        Password = password.HasHed(),
                        FullName = fullName,
                        IsDeleted = false,
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    RoleToUser newRoleUser = new()
                    {
                        UserId = newUser.UserId,
                        RoleId=3,
                    };
                    db.RoleToUsers.Add(newRoleUser);
                    db.SaveChanges();
                    MessageBox.Show("Customer successfully added");
                    FillUserGrid();
                    txtUserName.Text = "";
                    txtFullName.Text = "";
                    txtPassword.Text = "";

                }
                else
                {
                    MessageBox.Show("Such a user exists!");
                }
            }
            else
            {
                MessageBox.Show("Fill the all!");
            }
           
           
        }
         
        private void FillUserGrid()
        {
            dtgUsers.DataSource = db.Users.Select(x => new
            {
                x.UserId,
              x.FullName,
              x.UserName,
              x.Password,

            }).ToList();
            dtgUsers.Columns[0].Visible = false;
        }
        private void RegisterForm_Load(object sender, EventArgs e)
        {
            FillUserGrid();
            EnableButton("b");
        }
        private void EnableButton(string text)
        {
            if (text =="a")
            {
                btnRegister.Visible = false;
                btnEdit.Visible = true;
                btnDelete.Visible = true;
            }
            else
            {
                btnRegister.Visible = true;
                btnEdit.Visible = false;
                btnDelete.Visible = false;
            }
        }

        private void dtgUsers_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int userId = (int)dtgUsers.Rows[e.RowIndex].Cells[0].Value;
            selectedUser = db.Users.FirstOrDefault(x => x.UserId == userId);
            txtFullName.Text = selectedUser.FullName;
            txtUserName.Text = selectedUser.UserName;
            txtPassword.Text = selectedUser.Password;
            EnableButton("a");

           


        }
        private void ClearText()
        {
            txtFullName.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text;
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            int UsId = db.Users.FirstOrDefault(x => x.FullName == fullName).UserId;
            selectedUser.FullName = fullName;
            selectedUser.UserName = userName;
            selectedUser.Password = password.HasHed();
            db.SaveChanges();
            MessageBox.Show("The change was successfully saved", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            EnableButton("b");
            ClearText();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dg = MessageBox.Show("Are you sure to delete this?", "Deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dg == DialogResult.Yes)
            {
                selectedUser.IsDeleted = true;
                db.SaveChanges();
                FillUserGrid();
                ClearText();
                EnableButton("b");

            }
        }
    
    }
}
