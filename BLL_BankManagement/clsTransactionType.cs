using DAL_BankManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_BankManagement
{
    public class clsTransactionType
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TransactionTypeID { get; set; }
        public string TransactionTypeTitle { get; set; }
        public Decimal MinAmount { get; set; }
        public Decimal MaxAmount { get; set; }

        public clsTransactionType()
        {
            TransactionTypeID = 0;
            TransactionTypeTitle = string.Empty;
            MinAmount = 0;
            MaxAmount = 1000000;

            Mode = enMode.AddNew;
        }

        public clsTransactionType(int transactionTypeID, string transactionTypeTitle, Decimal minAmount, Decimal maxAmount)
        {
            TransactionTypeID = transactionTypeID;
            TransactionTypeTitle = transactionTypeTitle;
            MinAmount = minAmount;
            MaxAmount = maxAmount;

            Mode = enMode.Update;
        }

        public static DataTable GetTransactionTypes()
        {
            return clsTransactionTypeData.GetAllTransactionTypes();
        }

        private bool _UpdateTransactionType()
        {
            return clsTransactionTypeData.UpdateTransactionType(this.TransactionTypeID, this.TransactionTypeTitle, this.MinAmount, this.MaxAmount);
        }

        private bool _AddNewTransactionType()
        {
            this.TransactionTypeID = clsTransactionTypeData.AddNewTransactionType(this.TransactionTypeTitle, this.MinAmount, this.MaxAmount);

            return (this.TransactionTypeID != -1);
        }

        public static clsTransactionType Find(int TransactionTypeID)
        {
            string TransactionTypeTitle = string.Empty;
            decimal MinAmount = 0, MaxAmount = 0;

            if (clsTransactionTypeData.GetTransactionTypeInfoByID(TransactionTypeID, ref TransactionTypeTitle, ref MinAmount, ref MaxAmount))
                return new clsTransactionType(TransactionTypeID, TransactionTypeTitle, MinAmount, MaxAmount);
            else
                return null;
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTransactionType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTransactionType();

            }
            return false;
        }
    }
}