using DAL_BankManagement;
using System;
using System.Data;

namespace BLL_BankManagement
{
    public class clsUser
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }

        public int PersonID { get; set; }
        public clsPerson PersonInfo;

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsActive { get; set; }

        public clsUser()
        {
            this.UserID = -1;
            this.UserName = "";
            this.Password = "";
            this.PersonInfo = new clsPerson();
            this.IsActive = true;

            Mode = enMode.AddNew;
        }

        private clsUser(int userID, int personID, string userName, string password, bool isActive)
        {
            UserID = userID;
            PersonID = personID;
            this.PersonInfo = clsPerson.Find(PersonID);
            UserName = userName;
            Password = password;
            IsActive = isActive;

            Mode = enMode.Update;
        }

        private bool _AddNewUser()
        {
            //call DataAccess Layer 

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }

        private bool _UpdateUser()
        {
            //call DataAccess Layer 
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }

        public static clsUser FindByUserID(int UserID)
        {
            int personID = -1;
            string userName = "", password = "";
            bool isActive = false;

            return clsUserData.GetUserInfoByUserID(UserID, ref personID, ref userName, ref password, ref isActive)
                ? new clsUser(UserID, personID, userName, password, isActive)
                : null;
        }

        public static clsUser FindByUsernameAndPassword(string UserName, string Password)
        {
            int userID = -1, personID = -1;
            bool isActive = false;

            if (clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref userID, ref personID, ref isActive))
            {
                return new clsUser(userID, personID, UserName, Password, isActive);
            }
            else
            {
                return null;
            }
        }

        public bool Save()
        {
            // Strategy: Always ensure the Person is saved/updated first
            if (!this.PersonInfo.Save())
                return false;

            this.PersonID = this.PersonInfo.PersonID;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update; // Change mode to Update after successful addition
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }

        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }

        /// <summary>
        /// Performs a Soft Delete by setting IsActive to false.
        /// </summary>
        public static bool DeleteUser(int UserID)
        {
            // For security and auditing in banking, we deactivate the user instead of a hard delete.
            return clsUserData.DeactivateUser(UserID);
        }

        /// <summary>
        /// Deletes the User record and then the Person record (Hard Delete).
        /// </summary>
        public static bool HardDeleteUser(int UserID)
        {
            clsUser User = clsUser.FindByUserID(UserID);
            if (User != null)
            {
                // Delete User first to satisfy Foreign Key constraints
                if (clsUserData.DeleteUser(UserID))
                {
                    // Only delete the person if the user record was successfully removed
                    return clsPerson.DeletePerson(User.PersonID);
                }
            }
            return false;
        }

        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }

        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }

        public static bool IsUserExistByPersonID(int PersonID)
        {
            return clsUserData.IsUserExistByPersonID(PersonID);
        }

        public static bool ChangePassword(int UserID, string NewPassword)
        {
            return clsUserData.ChangePassword(UserID, NewPassword);
        }
    }
}