using Global_Classes;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using BLL_BankManagement;

namespace Classes
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\BMSLoginInfo";

            string UserNameValue = "UserName";
            string UserNameData = Username;

            string Passwordvalue = "Password";
            string PasswordData = Password;

            try
            {
                // Write the value to the Registry
                Registry.SetValue(keyPath, UserNameValue, UserNameData, RegistryValueKind.String);
                Registry.SetValue(keyPath, Passwordvalue, PasswordData, RegistryValueKind.String);

                return true;
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return false;
            }
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\BMSLoginInfo";
            string UserNameValue = "UserName";
            string Passwordvalue = "Password";

            try
            {
                // Read the value from the Registry
                string UserName = Registry.GetValue(keyPath, UserNameValue, null) as string;
                string password = Registry.GetValue(keyPath, Passwordvalue, null) as string;

                if ((UserName != null) && (password != null))
                {
                    if (UserName != "" && password != "")
                    {
                        Username = UserName;
                        Password = password;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.ExceptionLogger(ex, EventLogEntryType.Error);
                return false;
            }
        }
    }
}