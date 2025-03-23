using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateChangeClassLibrary.Entities
{
    public class TempChange
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public int Year { get; set; }
        public double? Change { get; set; }

        public TempChange(int id, string countryCode, string countryName, int year, double? change)
        {
            Id = id;
            CountryCode = countryCode;
            CountryName = countryName;
            Year = year;
            Change = change;
        }
    }
}
