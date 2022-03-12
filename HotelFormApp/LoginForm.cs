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
    public partial class LoginForm : Form
    {
        HotelDBContext db = new();
        User selectedUser;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text;
            string password = txtPassword.Text;
            string[] myArr = new string[] { userName, password };
            selectedUser = db.Users.FirstOrDefault(x => x.UserName == userName);
      
            if (Utilities.IsEmpty(myArr))
            {
                if (selectedUser!=null)
                {
                    var RoleName = db.RoleToUsers.Include(x=>x.Role).FirstOrDefault(x => x.UserId == selectedUser.UserId);
                    if (RoleName!=null && RoleName.Role.RoleName=="Admin")
                    {
                        if (password == selectedUser.Password)
                        {
                            AdminForm ad = new();
                            ad.ShowDialog();
                        }
                        else 
                        {
                            MessageBox.Show("Password isn't correct!");
                        }

                    }
                    else if (RoleName.Role.RoleName == "Worker")
                    {
                        if (password.HasHed() == selectedUser.Password)
                        {
                            Form1 fm = new(selectedUser);
                            fm.ShowDialog();
                            
                        }
                        else
                        {
                            MessageBox.Show("Password isn't correct!");
                        }

                    }
                    txtUserName.Text = "";
                    txtPassword.Text="";


                }
                else
                {
                    MessageBox.Show("Such a user does not exist");
                }

            }
            else
            {
                MessageBox.Show("Enter User Name and Password!");
            }
        }
    }
}
