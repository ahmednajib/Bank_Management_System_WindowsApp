using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DAL_BankManagement
{
    public class clsCountriesData
    {
        public static bool GetCountryIDByName(string CountryName, ref int CountryID)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetCountryIDByName", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CountryName", CountryName);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        CountryID = Convert.ToInt32(result);
                        IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    IsFound = false;
                }
            }
            return IsFound;
        }

        public static bool GetCountryNameByID(int CountryID, ref string CountryName)
        {
            bool IsFound = false;

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetCountryNameByID", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CountryID", CountryID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        CountryName = result.ToString();
                        IsFound = true;
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                    IsFound = false;
                }
            }
            return IsFound;
        }

        public static DataTable GetAllCountries()
        {
            DataTable dtCountries = new DataTable();

            using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("SP_GetAllCountries", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                try
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dtCountries.Load(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                }
            }
            return dtCountries;
        }
    }
}