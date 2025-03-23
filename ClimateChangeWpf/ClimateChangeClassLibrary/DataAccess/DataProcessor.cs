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
        public static DataSet ClimateChangeDataSet { get { return _climateChangeDataSet.Copy(); } }

        #region AanmakenDataSet
        static DataProcessor()
        {
            InitializeDataSet();
        }

        public static void InitializeDataSet()
        {
            _climateChangeDataSet = new DataSet();
            DataTable dtCountries = InitializeCountriesDataTable();
            DataTable dtTempChange = InitializeTempChangeDataTable();

            _climateChangeDataSet.Tables.Add(dtCountries);
            _climateChangeDataSet.Tables.Add(dtTempChange);
        }

        private static DataTable InitializeCountriesDataTable()
        {
            DataTable dtCountries = new DataTable(CountriesTableName);
            // Country,ImagesFileName,Alpha3,Region,SubRegion

            DataColumn countryName = new DataColumn("CountryName", typeof(string));
            DataColumn imagesFileName = new DataColumn("ImageFileName", typeof(string));
            DataColumn alpha3 = new DataColumn("CountryCode", typeof(string));
            DataColumn region = new DataColumn("Region", typeof(string));
            DataColumn subRegion = new DataColumn("SubRegion", typeof(string));

            dtCountries.Columns.Add(countryName);
            dtCountries.Columns.Add(imagesFileName);
            dtCountries.Columns.Add(alpha3);
            dtCountries.Columns.Add(region);
            dtCountries.Columns.Add(subRegion);

            DataColumn[] primaryKey = { alpha3 };
            dtCountries.PrimaryKey = primaryKey;

            AddRowsToDataTableFromFile(dtCountries, CountriesFilePath);

            return dtCountries;
        }

        private static DataTable InitializeTempChangeDataTable()
        {
            DataTable dtTempChange = new DataTable(TempChangeTableName);
            // CountryCode,CountryName,Year,TempChange, Id
            DataColumn id = new DataColumn("Id", typeof(int));
            DataColumn countryCode = new DataColumn("CountryCode", typeof(string));
            DataColumn countryName = new DataColumn("CountryName", typeof(string));
            DataColumn year = new DataColumn("Year", typeof(int));
            DataColumn tempChange = new DataColumn("TempChange", typeof(double));

            dtTempChange.Columns.Add(countryCode);
            dtTempChange.Columns.Add(countryName);
            dtTempChange.Columns.Add(year);
            dtTempChange.Columns.Add(tempChange);
            dtTempChange.Columns.Add(id);

            id.AutoIncrement = true;
            id.AutoIncrementSeed = 0;
            id.AutoIncrementStep = 1;

            tempChange.AllowDBNull = true;

            DataColumn[] primaryKey = { id };
            dtTempChange.PrimaryKey = primaryKey;

            AddRowsToDataTableFromFile(dtTempChange, TempChangeFilePath);

            return dtTempChange;
        }
        #endregion
        #region VullenDataSet
        private static void AddRowsToDataTableFromFile(DataTable dt, string filename)
        {
            AddRowsToDataTableFromFile(dt, filename, true);
        }

        private static void AddRowsToDataTableFromFile(DataTable dt, string filename, bool isSkippingFirstLine)
        {
            using (StreamReader sr = new StreamReader(filename))
            {
                if (isSkippingFirstLine) sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    dt.Rows.Add(sr.ReadLine().Split(',').Select(x => string.IsNullOrEmpty(x) ? null : x.Replace(".png", ";png").Replace(".", ",").Replace(";png", ".png")).ToArray());
                }
            }
        }
        #endregion

        public static DataView GetTempChangeDataView()
        {
            return _climateChangeDataSet.Tables[TempChangeTableName].DefaultView;
        }

        public static DataView GetCountriesDataView()
        {
            return _climateChangeDataSet.Tables[CountriesTableName].DefaultView;
        }

        public static List<TempChange> GetWorstYearsAfter2000()
        {
            return _climateChangeDataSet.Tables[TempChangeTableName].AsEnumerable()
                .Select(x =>
                {
                    double? change = null;
                    if (!String.IsNullOrWhiteSpace(x["TempChange"].ToString()))
                    {
                        change = Convert.ToDouble(x["TempChange"].ToString());
                    }
                    return new TempChange(
                    Convert.ToInt32(x["Id"].ToString()),
                    x["CountryCode"].ToString(),
                    x["CountryName"].ToString(),
                    Convert.ToInt32(x["Year"].ToString()),
                    change
                    );
                })
                .Where(x => x.Year > 2000 && x.Change > 1.2)
                .OrderByDescending(x => x.Change)
                .ToList();
        }

        public static List<Country> GetCountries()
        {
            return _climateChangeDataSet.Tables[CountriesTableName]
                .AsEnumerable()
                .Select(
                (dr) => new Country(
                                dr["CountryName"].ToString(),
                                dr["CountryCode"].ToString(),
                                dr["Region"].ToString(),
                                dr["SubRegion"].ToString(),
                                dr["ImageFileName"].ToString()
                                )
                ).ToList();
        }

        public static List<TempChange> GetTempChangesByCountryName(Country country)
        {
            List<TempChange> filteredTempChanges = _climateChangeDataSet
                .Tables[TempChangeTableName].AsEnumerable()
                .Where(x => x["CountryCode"].ToString().Equals(country.CountryCode))
                .OrderBy(x => Convert.ToInt32(x["Year"].ToString()))
                .Select(x =>
                {
                    double? change = null;
                    if (!String.IsNullOrWhiteSpace(x["TempChange"].ToString()))
                    {
                        change = Convert.ToDouble(x["TempChange"].ToString());
                    }
                    return new TempChange(
                    Convert.ToInt32(x["Id"].ToString()),
                    x["CountryCode"].ToString(),
                    x["CountryName"].ToString(),
                    Convert.ToInt32(x["Year"].ToString()),
                    change
                    );
                }
                ).ToList();
            return filteredTempChanges;
        }
    }
}
