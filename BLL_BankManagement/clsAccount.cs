using DAL_BankManagement;
using System.Data;

namespace BLL_BankManagement
{
    public class clsAccount
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int AccountID { get; set; }
        public int ClientID { get; set; }
        public string AccountNumber { get; set; }
        public string PinCode { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; }

        // Composition: Link to owner and creator
        public clsClient ClientInfo { get; set; }
        public clsUser CreatorInfo { get; set; }

        public clsAccount()
        {
            this.AccountID = -1;
            this.AccountNumber = "";
            this.PinCode = "";
            this.Balance = 0;
            this.IsActive = true;
            this.Mode = enMode.AddNew;
        }

        private clsAccount(int AccountID, int ClientID, string AccountNumber,
            string PinCode, decimal Balance, bool IsActive, int CreatedByUserID)
        {
            this.AccountID = AccountID;
            this.ClientID = ClientID;
            this.AccountNumber = AccountNumber;
            this.PinCode = PinCode;
            this.Balance = Balance;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.ClientInfo = clsClient.Find(ClientID);
            this.CreatorInfo = clsUser.FindByUserID(CreatedByUserID);
            this.Mode = enMode.Update;
        }

        public static clsAccount Find(string AccountNumber)
        {
            int AccountID = -1, ClientID = -1, CreatedByUserID = -1;
            string PinCode = ""; decimal Balance = 0; bool IsActive = false;

            if (clsAccountData.GetAccountInfoByAccountNumber(AccountNumber, ref AccountID, ref ClientID,
                ref PinCode, ref Balance, ref IsActive, ref CreatedByUserID))
            {
                return new clsAccount(AccountID, ClientID, AccountNumber, PinCode, Balance, IsActive, CreatedByUserID);
            }
            return null;
        }

        // Static method to find by ID
        public static clsAccount Find(int AccountID)
        {
            int ClientID = -1, CreatedByUserID = -1;
            string AccountNumber = "", PinCode = "";
            decimal Balance = 0;
            bool IsActive = false;

            if (clsAccountData.GetAccountInfoByID(AccountID, ref ClientID, ref AccountNumber,
                ref PinCode, ref Balance, ref IsActive, ref CreatedByUserID))
            {
                // This calls the private constructor that sets Mode to Update
                return new clsAccount(AccountID, ClientID, AccountNumber, PinCode, Balance, IsActive, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }

        private bool _AddNewAccount()
        {
            // Pass the properties of the current object to the Data Layer
            this.AccountID = clsAccountData.AddNewAccount(
                this.ClientID,
                this.AccountNumber,
                this.PinCode,
                this.Balance,
                this.IsActive,
                this.CreatedByUserID
            );

            // If the ID is not -1, the insertion was successful
            return (this.AccountID != -1);
        }

        private bool _UpdateAccount()
        {
            // Pass the updated properties to the Data Layer for the existing ID
            return clsAccountData.UpdateAccount(
                this.AccountID,
                this.PinCode,
                this.IsActive,
                this.Balance
            );
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewAccount())
                    {
                        // After adding, we switch to Update mode so future saves update the record
                        Mode = enMode.Update;
                        return true;
                    }
                    return false;

                case enMode.Update:
                    return _UpdateAccount();

                default:
                    return false;
            }
        }

        // Returns a DataTable to bind directly to dgvAccounts.DataSource
        public static DataTable GetAllAccounts()
        {
            return clsAccountData.GetAllAccounts();
        }

        // Recommended for standard use: keeps the account in the DB but marks it inactive
        public static bool SoftDelete(int AccountID)
        {
            return clsAccountData.SoftDeleteAccount(AccountID);
        }

        // Use with caution: permanently removes the account record
        public static bool Delete(int AccountID)
        {
            return clsAccountData.DeleteAccount(AccountID);
        }

        public static bool IsAccountExist(string AccountNumber)
        {
            return clsAccountData.IsAccountExist(AccountNumber);
        }

        public static bool IsAccountActive(string AccountNumber)
        {
            return clsAccountData.IsAccountActive(AccountNumber);
        }
    }
}