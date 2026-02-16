using DAL_BankManagement;
using System;
using System.Collections.Generic;
using System.Data;

namespace BLL_BankManagement
{
    public class clsClient
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; } // Composition
        public DateTime DateJoined { get; set; }
        public bool IsActive { get; set; } // Added Property

        public clsClient()
        {
            this.ClientID = -1;
            this.PersonID = -1;
            this.PersonInfo = new clsPerson();
            this.DateJoined = DateTime.Now;
            this.IsActive = true; // Default to active

            Mode = enMode.AddNew;
        }

        private clsClient(int clientID, int personID, DateTime DateJoined, bool IsActive)
        {
            this.ClientID = clientID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.DateJoined = DateJoined;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }

        private bool _AddNewClient()
        {
            // Update DAL call to include IsActive if your AddNew SP supports it
            this.ClientID = clsClientData.AddNewClient(this.PersonID, this.DateJoined, this.IsActive);
            return (this.ClientID != -1);
        }

        private bool _UpdateClient()
        {
            return clsClientData.UpdateClient(this.ClientID, this.PersonID, this.DateJoined, this.IsActive);
        }

        public static clsClient Find(int ClientID)
        {
            int personID = -1;
            DateTime DateJoined = DateTime.Now;
            bool IsActive = false;

            if (clsClientData.GetClientInfoByID(ClientID, ref personID, ref DateJoined, ref IsActive))
            {
                return new clsClient(ClientID, personID, DateJoined, IsActive);
            }
            else
                return null;
        }

        public bool Save()
        {
            if (!this.PersonInfo.Save())
                return false;

            this.PersonID = this.PersonInfo.PersonID;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewClient())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else return false;

                case enMode.Update:
                    return _UpdateClient();
            }
            return false;
        }

        public static DataTable GetAllClients()
        {
            // You can choose to call a "GetAllActiveClients" DAL method here 
            // if you want the dashboard to only show active ones.
            return clsClientData.GetAllClients();
        }

        /// <summary>
        /// Performs a Soft Delete by setting IsActive to false.
        /// </summary>
        public static bool DeleteClient(int ClientID)
        {
            // In a Soft Delete model, we don't delete the Person record.
            // We only deactivate the Client relationship.
            return clsClientData.DeactivateClient(ClientID);
        }

        /// <summary>
        /// Use this only if you truly want to remove the records from the DB.
        /// </summary>
        public static bool HardDeleteClient(int ClientID)
        {
            clsClient Client = clsClient.Find(ClientID);
            if (Client != null)
            {
                // Delete Client Link first (Checks for Account Foreign Keys)
                if (clsClientData.DeleteClient(ClientID))
                {
                    // Then delete personal profile
                    return clsPerson.DeletePerson(Client.PersonID);
                }
            }
            return false;
        }

        public static bool IsClientExist(int ClientID)
        {
            return clsClientData.IsClientExist(ClientID);
        }
    }
}