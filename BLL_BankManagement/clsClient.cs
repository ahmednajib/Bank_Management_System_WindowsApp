using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BankManagement
{
    public class clsClient
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int ClientID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public DateTime DateJoined { get; set; }

        public clsClient()
        {
            this.ClientID = -1;
            this.PersonID = -1;
            this.DateJoined = DateTime.Now;

            Mode = enMode.AddNew;
        }

        public clsClient(int clientID, int personID, DateTime DateJoined)
        {
            this.ClientID = clientID;
            this.PersonID = personID;
            this.PersonInfo = clsPerson.Find(personID);
            this.DateJoined = DateJoined;

            Mode = enMode.Update;
        }
    }
}