using System;
using DAL_BankManagement;

namespace BLL_BankManagement
{
    public class clsTransaction
    {
        public enum enTransactionType { Deposit = 1, Withdraw = 2, Transfer = 3 };

        public int TransactionID { get; private set; }
        public enTransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public int SenderAccountID { get; set; }
        public int ReceiverAccountID { get; set; }
        public int CreatedByUserID { get; set; }

        public clsTransaction()
        {
            this.TransactionID = -1;
            this.Amount = 0;
            this.Description = "";
            this.SenderAccountID = 0;
            this.ReceiverAccountID = 0;
        }

        // Public execution method
        public bool Execute(enTransactionType TransactionType)
        {
            this.TransactionType = TransactionType;

            switch (this.TransactionType)
            {
                case enTransactionType.Deposit:
                    return _PerformDeposit();

                case enTransactionType.Withdraw:
                    return _PerformWithdrawal();

                case enTransactionType.Transfer:
                    return _PerformTransfer();

                default:
                    return false;
            }
        }

        // --- Private Logic Functions ---

        private bool _PerformDeposit()
        {
            // For Deposits: Sender is 0 (System), Receiver is the target account
            this.TransactionID = clsTransactionData.ExecuteTransaction(
                (int)enTransactionType.Deposit,
                this.Amount,
                this.Description,
                0,
                this.ReceiverAccountID,
                this.CreatedByUserID
            );

            return (this.TransactionID != -1);
        }

        private bool _PerformWithdrawal()
        {
            // For Withdrawals: Receiver is 0 (System), Sender is the target account
            this.TransactionID = clsTransactionData.ExecuteTransaction(
                (int)enTransactionType.Withdraw,
                this.Amount,
                this.Description,
                this.SenderAccountID,
                0,
                this.CreatedByUserID
            );

            return (this.TransactionID != -1);
        }

        private bool _PerformTransfer()
        {
            // For Transfers: Both Sender and Receiver IDs are required
            if (this.SenderAccountID == this.ReceiverAccountID) return false;

            this.TransactionID = clsTransactionData.ExecuteTransaction(
                (int)enTransactionType.Transfer,
                this.Amount,
                this.Description,
                this.SenderAccountID,
                this.ReceiverAccountID,
                this.CreatedByUserID
            );

            return (this.TransactionID != -1);
        }
    }
}