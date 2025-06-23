using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace To_Do_List
{
    public partial class frmManageUsers : Form
    {
        public frmManageUsers()
        {
            InitializeComponent();
            _LoadUsers();
        }

        private void _LoadUsers()
        {
            dgvUsers.Rows.Clear();
            if (!File.Exists(clsUsers.FileName)) return;

            var lines = File.ReadAllLines(clsUsers.FileName);
            dgvUsers.Columns[0].Width = 150;
            dgvUsers.Columns[1].Width = 300;

            foreach (var line in lines)
            {
                string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                if (parts.Length >= 2)
                    dgvUsers.Rows.Add(parts[0], parts[1]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username:", "Add User", "");
            string password = Microsoft.VisualBasic.Interaction.InputBox("Enter Password:", "Add User", "");

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return;

            clsUsers user = new clsUsers(clsUsers.enMode.AddNew, username, password);
            if (user.Save() == clsUsers.enSaveResult.Success)
            {
                MessageBox.Show("User added!");
                _LoadUsers();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username to update:", "Update User", "");
            var user = clsUsers.Find(username);
            if (user == null)
            {
                MessageBox.Show("User not found.");
                return;
            }

            string newPassword = Microsoft.VisualBasic.Interaction.InputBox("Enter new password:", "Update Password", user.Password);
            if (string.IsNullOrWhiteSpace(newPassword)) return;

            user.Password = newPassword;
            if (user.Save() == clsUsers.enSaveResult.Success)
            {
                MessageBox.Show("User updated!");
                _LoadUsers();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username to delete:", "Delete User", "");
            clsUsers user = new clsUsers(clsUsers.enMode.Update, username, "");
            if (user.Delete(username))
            {
                MessageBox.Show("User deleted.");
                _LoadUsers();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string username = Microsoft.VisualBasic.Interaction.InputBox("Enter Username to find:", "Find User", "");
            var user = clsUsers.Find(username);
            if (user != null)
            {
                MessageBox.Show($"User Found:\n\nUsername: {user.UserName}\nPassword: {user.Password}", "Found");
            }
            else
            {
                MessageBox.Show("User not found.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
