using System;

namespace BLL_BankManagement
{
    public class clsTransaction
    {
        enum enTransactionType { Deposit = 1, Withdrawal = 2, Transfer = 3 }
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TransactionID { get; set; }
        public int TransactionTypeID { get; set; }
        public clsTransactionType TransactionTypeInfo { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Description { get; set; }
        public int SenderAccountID { get; set; }
        //public clsAccount SenderAccountInfo { get; set; }
        public int ReceiverAccountID { get; set; }
        //public clsAccount ReceiverAccountInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }

        public clsTransaction()
        {
            this.TransactionID = -1;
            this.TransactionTypeID = -1;
            this.Amount = 0;
            this.TransactionDate = DateTime.Now;
            this.Description = string.Empty;
            this.SenderAccountID = -1;
            this.ReceiverAccountID = -1;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }

        public clsTransaction(int transactionID, int transactionTypeID, decimal amount, DateTime transactionDate, string description, int senderAccountID, int receiverAccountID, int createdByUserID)
        {
            this.TransactionID = transactionID;
            this.TransactionTypeID = transactionTypeID;
            this.TransactionTypeInfo = clsTransactionType.Find(transactionTypeID);
            this.Amount = amount;
            this.TransactionDate = transactionDate;
            this.Description = description;
            this.SenderAccountID = senderAccountID;
            //this.SenderAccountInfo = clsAccount.Find(senderAccountID);
            this.ReceiverAccountID = receiverAccountID;
            //this.ReceiverAccountInfo = clsAccount.Find(receiverAccountID);
            this.CreatedByUserID = createdByUserID;
            this.CreatedByUserInfo = clsUser.Find(createdByUserID);

            Mode = enMode.Update;
        }
    }
}