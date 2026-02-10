using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BankManagement
{
    public class clsTransactionTypeData
    {
        public static int AddNewTransactionType(string TransactionTypeTitle, decimal MinAmount, decimal MaxAmount)
        {
            int TransactionTypeID = -1;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewTransactionType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TransactionTypeTitle", TransactionTypeTitle);
                command.Parameters.AddWithValue("@MinAmount", MinAmount);
                command.Parameters.AddWithValue("@MaxAmount", MaxAmount);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    {
                        TransactionTypeID = insertedID;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return TransactionTypeID;
        }

        public static DataTable GetAllTransactionTypes()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllTransactionTypes", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows) dt.Load(reader);
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return dt;
        }

        public static bool UpdateTransactionType(int TransactionTypeId, string TransactionTypeTitle, decimal MinAmount, decimal MaxAmount)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateTransactionType", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TransactionTypeID", TransactionTypeId);
                command.Parameters.AddWithValue("@TransactionTypeTitle", TransactionTypeTitle);
                command.Parameters.AddWithValue("@MinAmount", MinAmount);
                command.Parameters.AddWithValue("@MaxAmount", MaxAmount);

                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    return false;
                }
            }
            return rowsAffected > 0;
        }

        public static bool GetTransactionTypeInfoByID(int TransactionTypeId, ref string TransactionTypeTitle, ref decimal MinAmount, ref decimal MaxAmount)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetTransactionTypeInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@TransactionTypeID", TransactionTypeId);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            TransactionTypeTitle = (string)reader["TransactionTypeTitle"];
                            MinAmount = Convert.ToDecimal(reader["MinAmount"]);
                            MinAmount = Convert.ToDecimal(reader["MaxAmount"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    isFound = false;
                }
            }
            return isFound;
        }
    }
}