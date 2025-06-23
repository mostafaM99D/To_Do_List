using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "" || txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Empty Username/Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsUsers.Find(txtUsername.Text,txtPassword.Text) != null)
            {
                frmMain frm = new frmMain();
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }else
            {
                MessageBox.Show("Invalid Username/Password.","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                txtPassword.Text = "";
                txtUsername.Text = "";
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
              {
                e.SuppressKeyPress = true; 
                btnLogin_Click(sender, e);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddNew frm =new frmAddNew();
            this.Hide();
            frm.ShowDialog();
            frm .Close();
            this.ShowDialog();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Select();
        }
    }
}
