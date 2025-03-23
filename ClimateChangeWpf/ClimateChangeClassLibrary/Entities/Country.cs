using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClimateChangeClassLibrary.Entities
{
    public class Country
    {
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string ImageFilePath { get; set; }

        public Country(string countryName, string countryCode, string region, string subRegion, string imageFilePath)
        {
            CountryName = countryName;
            CountryCode = countryCode;
            Region = region;
            SubRegion = subRegion;
            ImageFilePath = imageFilePath;
        }

        public override string ToString()
        {
            return CountryName;
        }
    }
}
