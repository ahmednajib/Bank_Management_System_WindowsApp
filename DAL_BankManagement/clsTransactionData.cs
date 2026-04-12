using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL_BankManagement
{
    public class clsTransactionData
    {
        public static int ExecuteTransaction(int TypeID, decimal Amount, string Description,
            int SenderID, int ReceiverID, int UserID)
        {
            int TransactionID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_PerformTransaction", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TransactionTypeID", TypeID);
                command.Parameters.AddWithValue("@Amount", Amount);
                command.Parameters.AddWithValue("@Description", Description);
                command.Parameters.AddWithValue("@SenderAccountID", SenderID);
                command.Parameters.AddWithValue("@ReceiverAccountID", ReceiverID);
                command.Parameters.AddWithValue("@CreatedByUserID", UserID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null) TransactionID = Convert.ToInt32(result);
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, System.Diagnostics.EventLogEntryType.Error);
                }
            }
            return TransactionID;
        }
    }
}