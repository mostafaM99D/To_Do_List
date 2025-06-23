# 📝 To-Do List Manager (C# WinForms)

A simple and clean To-Do List app built with **Windows Forms** and **C#**, including full **User Management**, task tracking, and file-based storage.

---

## ✅ Features

- 🔐 **Login/Register System**
- 📋 **Manage To-Do Missions (CRUD)**
- 👤 **Manage Users (CRUD)**
- 💾 Data stored in `.txt` files (`Users.txt`, `Missions.txt`)
- 📊 Data displayed in `DataGridView`
- Simple UI using WinForms (no database needed)

---

## 🔄 CRUD Functionality

### Users
| Action   | Description             |
|----------|-------------------------|
| Add      | Register new user       |
| Update   | Change password         |
| Delete   | Remove user             |
| Find     | Find user by username   |

### Missions
| Action   | Description              |
|----------|--------------------------|
| Add      | Create new mission       |
| Update   | Edit mission by ID       |
| Delete   | Remove mission by ID     |
| Find     | Search mission by ID     |

---

## 🛠️ Technologies Used

- C#
- .NET Framework (WinForms)
- Visual Studio
- File-based data storage

---

## 📁 Project Structure

- `frmLogin.cs`: Login form for user authentication
- `frmAddNew.cs`: User registration form
- `frmMain.cs`: Main screen to manage tasks
- `frmManageUsers.cs`: Separate form to manage user accounts
- `clsUsers.cs`: Handles user logic (CRUD, file storage)
- `clsMissions.cs`: Handles mission logic (CRUD, file storage)
- `Users.txt`: Stores usernames and passwords
- `Missions.txt`: Stores mission ID and text

---

## 💡 How Data is Stored

Each line in the text files follows this format:

**Users.txt**

 username#//#password
 
**Missions.txt**

 ID#//#MissionText
