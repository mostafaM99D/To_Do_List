using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    public partial class frmAddNew : Form
    {
        public frmAddNew()
        {
            InitializeComponent();
            btnBackToLoginScreen.Enabled = false;
        }

        private void _Return()
        {
            txtUsername.Enabled=false;
            txtPassword.Enabled=false;
            btnAddNew.Enabled=false;
            btnBackToLoginScreen.Enabled=true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text.Trim()==""||txtPassword.Text.Trim()=="")
            {
                MessageBox.Show("Empty Username/Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (clsUsers.Find(txtUsername.Text, txtPassword.Text) == null)
            {
                clsUsers user = new clsUsers(clsUsers.enMode.AddNew,txtUsername.Text,txtPassword.Text);
                switch (user.Save())
                {
                    case clsUsers.enSaveResult.Success:
                        MessageBox.Show($"User With {user.UserName} added successfully");
                        _Return();
                        break;
                    case clsUsers.enSaveResult.Failure:
                        MessageBox.Show($"Error during adding user with {user.UserName}.");
                        break;

                }
            }
            else
            {
                MessageBox.Show($"User With {txtUsername.Text} is already exist", "info", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {    e.SuppressKeyPress = true;
             button2_Click(sender, e); }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                txtPassword.Select();
            }
        }

        private void btnBackToLoginScreen_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
