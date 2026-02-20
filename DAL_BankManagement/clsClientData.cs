using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAL_BankManagement
{
    public class clsClientData
    {
        public static bool GetClientInfoByID(int ClientID, ref int PersonID, ref DateTime DateJoined, ref bool IsActive)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetClientInfoByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            PersonID = (int)reader["PersonID"];
                            DateJoined = (DateTime)reader["DateJoined"];
                            IsActive = (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }

        public static bool GetClientInfoByNationalNumber(string NationalNo, ref int ClientID, ref int PersonID, ref DateTime DateJoined, ref bool IsActive)
        {
            bool isFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetClientInfoByNationalNo", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@NationalNo", NationalNo);

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isFound = true;
                            ClientID = (int)reader["ClientID"];
                            PersonID = (int)reader["PersonID"];
                            DateJoined = (DateTime)reader["DateJoined"];
                            IsActive = (bool)reader["IsActive"];
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Using your existing exception logger
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }

            return isFound;
        }

        public static int AddNewClient(int PersonID, DateTime DateJoined, bool IsActive)
        {
            int ClientID = -1;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_AddNewClient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@DateJoined", DateJoined);
                command.Parameters.AddWithValue("@IsActive", IsActive);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && int.TryParse(result.ToString(), out int insertedID))
                        ClientID = insertedID;
                }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return ClientID;
        }

        public static bool UpdateClient(int ClientID, int PersonID, DateTime DateJoined, bool IsActive)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_UpdateClient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);
                command.Parameters.AddWithValue("@PersonID", PersonID);
                command.Parameters.AddWithValue("@DateJoined", DateJoined);
                command.Parameters.AddWithValue("@IsActive", IsActive);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return (rowsAffected > 0);
        }

        public static bool DeactivateClient(int ClientID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_SetClientInactive", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return (rowsAffected > 0);
        }

        public static DataTable GetAllClients()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllClients", connection))
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
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return dt;
        }

        public static bool DeleteClient(int ClientID)
        {
            int rowsAffected = 0;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_DeleteClient", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);

                try { connection.Open(); rowsAffected = command.ExecuteNonQuery(); }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return (rowsAffected > 0);
        }

        public static bool IsClientExist(int ClientID)
        {
            bool isFound = false;
            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_IsClientExistByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ClientID", ClientID);
                try { connection.Open(); isFound = command.ExecuteScalar() != null; }
                catch (Exception ex) { clsLogger.ExceptionLogger(ex, EventLogEntryType.Error); }
            }
            return isFound;
        }
    }
}