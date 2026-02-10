using System;
using System.Diagnostics;

namespace DAL_BankManagement
{
    public static class clsLogger
    {
        // Specify the source name for the event log
        private static string sourceName = "BMS";

        public static void ExceptionLogger(Exception ex, EventLogEntryType type)
        {
            // Create the event source if it does not exist
            if (!EventLog.SourceExists(sourceName))
            {
                EventLog.CreateEventSource(sourceName, "Application");
            }

            // Log an information event
            EventLog.WriteEntry(sourceName, _FormatErrorMessage(ex), type);
        }

        private static string _FormatErrorMessage(Exception ex)
        {
            string message =
                 $"--- Exception Log ---\n\n" +
                 $"Timestamp: {DateTime.Now}\n\n" +
                 $"Message: {ex.Message}\n\n" +
                 $"Inner Exception: {(ex.InnerException != null ? ex.InnerException.Message : "N/A")}\n\n" +
                 $"Stack Trace: {ex.StackTrace}\n\n" +
                 $"Source: {ex.Source}\n\n" +
                 $"-----------------------";

            return message;
        }
    }
}