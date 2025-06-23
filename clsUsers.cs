using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace To_Do_List
{
    public class clsUsers
    {


        public enum enMode { AddNew = 1, Update = 2 };
        private enMode _Mode;
        public string UserName { get; set; }
        public string Password { get; set; }

        public bool MarkForDelete = false;
        public static string FileName = Path.Combine(Directory.GetParent(Application.StartupPath).Parent.FullName,"Users.txt");

        static public List<clsUsers> _Users;

        public clsUsers(enMode Mode, string Username, string Password)
        {
            this.UserName = Username;
            this.Password = Password;
            this._Mode = Mode;
        }


        private bool _AddToFile()
        {
            if (!File.Exists(FileName))
                File.Create(FileName).Close();

            if (Find(UserName) != null)
            {
                MessageBox.Show("The User Already Exists.");
                return false;
            }

            using (StreamWriter writer = new StreamWriter(FileName, true))
            {
                writer.WriteLine($"{this.UserName}#//#{this.Password}");
            }

            return true;
        }


        private static clsUsers _GetUser(string line)
        {
            string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
            return new clsUsers(enMode.Update, parts[0], parts[1]);
        }

        private static List<clsUsers> _LoadFromFile()
        {
            List<clsUsers> res = new List<clsUsers>();

            using (StreamReader reader = new StreamReader(FileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    res.Add(_GetUser(line));
                }
            }
            return res;

        }


        private bool _AddNew()
        {
            try
            {
                return _AddToFile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }


        private void _SaveToFile()
        {
            using (StreamWriter writer = new StreamWriter(FileName))
            {
                foreach (var item in _Users)
                {
                    if (!item.MarkForDelete)
                        writer.WriteLine($"{item.UserName}#//#{item.Password}");
                }
            }
        }

        private bool _Update()
        {
            try
            {
                _Users = _LoadFromFile();

                foreach (var user in _Users)
                {
                    if (user.UserName == this.UserName)
                    {
                        user.Password = this.Password;
                        _SaveToFile();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
                return false;
            }
        }

        public bool Delete(string UserName)
        {
            _Users = _LoadFromFile();
            foreach (var user in _Users)
            {
                if (user.UserName == UserName)
                {
                    user.MarkForDelete = true;
                    _SaveToFile();
                    return true;
                }
            }
            MessageBox.Show("The User Not Founded .");
            return false;
        }

        public static clsUsers Find(string username)
        {
            if(!File.Exists(FileName))
                return null;
            using (StreamReader reader =new StreamReader(FileName))
            {
                string line;
                while((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    if (parts[0]==username)
                        return new clsUsers(enMode.Update,parts[0], parts[1]);
                }
            }

            return null;
        }
        public static clsUsers Find(string username,string password)
        {
            if (!File.Exists(FileName))
                return null;
            using (StreamReader reader = new StreamReader(FileName))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(new string[] { "#//#" }, StringSplitOptions.None);
                    if (parts[0] == username && parts[1]==password)
                        return new clsUsers(enMode.Update, parts[0], parts[1]);
                }
            }

            return null;
        } 

        public enum enSaveResult { Success = 1, Failure = 0 }
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
                    return enSaveResult.Failure;
                case enMode.Update:
                    if (_Update())
                        return enSaveResult.Success;

                    return enSaveResult.Failure;
            }
            return enSaveResult.Failure;

        }

    }
}
