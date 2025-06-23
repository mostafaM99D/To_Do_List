using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace To_Do_List
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            _LoadMissions();
        }

        private void _LoadMissions()
        {
            dgvMissions.Rows.Clear();
            if (!File.Exists(clsMissions.FileName)) return;

            var lines = File.ReadAllLines(clsMissions.FileName);
            dgvMissions.Columns[0].Width = 60;
            dgvMissions.Columns[1].Width = 900; 
            foreach (var line in lines)
            {
                string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                if (parts.Length >= 2 && int.TryParse(parts[0], out int id))
                    dgvMissions.Rows.Add(id, parts[1]);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Enter Mission Text:", "Add Mission", "");
            if (string.IsNullOrWhiteSpace(input)) return;

            clsMissions mission = new clsMissions(clsMissions.enMode.AddNew, 0, input);
            if (mission.Save() == clsMissions.enSaveResult.Success)
            {
                MessageBox.Show("Mission added!");
                _LoadMissions();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string ids = Microsoft.VisualBasic.Interaction.InputBox("Enter ID to update:", "Update Mission", "");
            if (!int.TryParse(ids, out int id)) return;

            var mission = clsMissions.Find(id);
            if (mission == null)
            {
                MessageBox.Show("Mission not found.");
                return;
            }

            string newText = Microsoft.VisualBasic.Interaction.InputBox("Enter new mission text:", "Update", mission.MissionText);
            if (string.IsNullOrWhiteSpace(newText)) return;

            mission.MissionText = newText;
            if (mission.Save() == clsMissions.enSaveResult.Success)
            {
                MessageBox.Show("Mission updated!");
                _LoadMissions();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string idStr = Microsoft.VisualBasic.Interaction.InputBox("Enter ID to delete:", "Delete Mission", "");
            if (!int.TryParse(idStr, out int id)) return;

            clsMissions mission = new clsMissions(clsMissions.enMode.Update, id, "");
            if (mission.Delete(id))
            {
                MessageBox.Show("Mission deleted.");
                _LoadMissions();
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string idStr = Microsoft.VisualBasic.Interaction.InputBox("Enter ID to find:", "Find Mission", "");
            if (!int.TryParse(idStr, out int id)) return;

            var mission = clsMissions.Find(id);
            if (mission != null)
                MessageBox.Show($"Mission Found:\n\nID: {mission.ID}\nText: {mission.MissionText}", "Found");
            else
                MessageBox.Show("Mission not found.");
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmManageUsers frm = new frmManageUsers();
            frm.ShowDialog();
            this.Show();
        }
    }
}