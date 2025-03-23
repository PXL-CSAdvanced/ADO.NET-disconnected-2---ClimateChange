using ClimateChangeClassLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateChangeClassLibrary.DataAccess
{
    public static class DataProcessor
    {
        private const string CountriesTableName = "Countries";
        private const string TempChangeTableName = "TempChange";
        private const string TempChangeFilePath = "resources/Temperature_change.csv";
        private const string CountriesFilePath = "resources/Country_codes_and_flags.csv";

        private static DataSet _climateChangeDataSet;

        #region AanmakenDataSet
        static DataProcessor()
        {
            InitializeDataSet();
        }

        public static void InitializeDataSet()
        {
            throw new NotImplementedException();
            // TODO: Gebruik de methodes InitializeCountriesDataTable() en InitializeTempChangeDataTable()
            // om er voor te zorgen dat de DataTables in deze DataSet correct aangemaakt zijn.
        }

        private static DataTable InitializeCountriesDataTable()
        {
            throw new NotImplementedException();
        }

        private static DataTable InitializeTempChangeDataTable()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region VullenDataSet
        private static void AddRowsToDataTableFromFile(DataTable dt, string filename)
        {
            throw new NotImplementedException();
        }
        #endregion

        public static DataView GetTempChangeDataView()
        {
            throw new NotImplementedException();
        }

        public static DataView GetCountriesDataView()
        {
            throw new NotImplementedException();
        }

        public static List<TempChange> GetWorstYearsAfter2000()
        {
            throw new NotImplementedException();
        }

        public static List<Country> GetCountries()
        {
            throw new NotImplementedException();
        }

        public static List<TempChange> GetTempChangesByCountryName(Country country)
        {
            throw new NotImplementedException();
        }
    }
}
