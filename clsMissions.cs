using System;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    public class clsMissions
    {

        public enum enMode { AddNew = 1, Update = 2 }
        private enMode _Mode;
        public string MissionText { get; set; }
        public bool MarkForDelete = false;

        public int ID { get; set; } = 0;
        public static string FileName = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName, "Missions.txt");
        private static List<clsMissions> _Missions;


        public clsMissions(enMode mode, int ID, string MissionText)
        {
            this._Mode = mode;
            this.MissionText = MissionText;
            this.ID = ID;
        }

        private static clsMissions _GetMission(string line)
        {
            string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
            if (parts.Length < 2) return null;
            if (!int.TryParse(parts[0], out int id)) return null;
            return new clsMissions(enMode.Update, int.Parse(parts[0]), parts[1]);
        }

        private static List<clsMissions> _LoadMissionsFromFile()
        {
            List<clsMissions> rres = new List<clsMissions>();
            using (StreamReader sr = new StreamReader(FileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    rres.Add(_GetMission(line));
                }
                return rres;
            }
        }

        private int _GetLastID()
        {
            if (!File.Exists(FileName)) return 0;

            var lines = File.ReadAllLines(FileName);
            if (lines.Length == 0) return 0;

            var lastLine = lines.Last();
            var parts = lastLine.Split(new string[] { "#//#" }, StringSplitOptions.None);
            if (int.TryParse(parts[0], out int lastID))
                return lastID;

            return 0;
        }

        private bool _AddMissionToFile()
        {
            try
            {
                using (StreamWriter sr = new StreamWriter(FileName, true))
                {
                    sr.WriteLine($"{this.ID}#//#{this.MissionText}");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }



        private bool _AddNew()
        {
            try
            {
                this.ID = _GetLastID() + 1;
                return _AddMissionToFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }
        private void _SaveInFile()
        {
            using (StreamWriter sr = new StreamWriter(FileName))
            {
                foreach (var mission in _Missions)
                {
                    if (!mission.MarkForDelete)
                    {
                        sr.WriteLine($"{mission.ID}#//#{mission.MissionText}");
                    }
                }
            }
        }
        private bool _Update()
        {
            _Missions = _LoadMissionsFromFile();
            foreach (var mission in _Missions)
            {
                if (mission.ID == this.ID)
                {
                    mission.MissionText = this.MissionText;
                    _SaveInFile();
                    return true;
                }
            }
            return false;
        }
        public bool Delete(int ID)
        {
            try
            {
                _Missions = _LoadMissionsFromFile();
                foreach (var mission in _Missions)
                {
                    if (mission.ID == ID)
                    {
                        mission.MarkForDelete = true;
                        break;
                    }
                }
                _SaveInFile();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static clsMissions Find(int ID)
        {
            if (!File.Exists(FileName))
                File.Create(FileName).Close();

            using (var reader = new StreamReader(FileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    if (int.TryParse(parts[0], out int id))
                    {
                        if (id == ID)
                        {
                            return new clsMissions(enMode.Update, id, parts[1]);
                        }
                    }

                }
            }
            return null;

        }


        public enum enSaveResult { Success = 0, Error = 1 }

        public enSaveResult Save()
        {
            switch (_Mode)
            {

                case enMode.AddNew:

                    if (_AddNew())
                    {
                        _Mode = enMode.Update;
                        return enSaveResult.Success;
                    }
                    break;

                case enMode.Update:

                    break;

                default:
                    return enSaveResult.Error;

            }
            return enSaveResult.Error;

        }

    }
}
