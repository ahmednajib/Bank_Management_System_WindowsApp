using System.Data;
using DAL_BankManagement;

namespace BLL_BankManagement
{
    public class clsCountries
    {
        public int CountryID { set; get; }
        public string CountryName { set; get; }

        public clsCountries()
        {
            this.CountryID = -1;
            this.CountryName = "";
        }

        private clsCountries(int ID, string CountryName)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
        }

        public static clsCountries Find(string CountryName)
        {
            int CountryID = -1;

            if (clsCountriesData.GetCountryIDByName(CountryName, ref CountryID))

                return new clsCountries(CountryID, CountryName);
            else
                return null;

        }

        public static clsCountries Find(int CountryID)
        {
            string CountryName = "";

            if (clsCountriesData.GetCountryNameByID(CountryID, ref CountryName))

                return new clsCountries(CountryID, CountryName);
            else
                return null;
        }

        public static DataTable GetAllCountries()
        {
            return clsCountriesData.GetAllCountries();
        }
    }
}
