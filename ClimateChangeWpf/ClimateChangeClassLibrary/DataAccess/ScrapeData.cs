using System.Drawing.Imaging;
using System.Net;
using Svg;

namespace ClimateChangeClassLibrary.DataAccess
{
    public static class ScrapeData
    {
        #region ScrapeMethods
        private static void RewriteTempForCountryCode()
        {
            List<string> countryCodes = new List<string>();
            using (StreamReader sr = new StreamReader("Country_codes_and_flags.csv"))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    countryCodes.Add(sr.ReadLine().Split(',')[2]);
                }
            }

            using (StreamReader sr = new StreamReader("Temperature_change_Data.csv"))
            {
                using (StreamWriter sw = new StreamWriter("Temperature_change.csv"))
                {
                    sw.WriteLine(sr.ReadLine());
                    while (!sr.EndOfStream)
                    {
                        string csv = sr.ReadLine();
                        string[] values = csv.Split(',');
                        if (countryCodes.Contains(values[0]))
                        {
                            sw.WriteLine(csv);
                        }
                    }

                }
            }
        }

        private static void RewriteFlagFile()
        {
            using (StreamReader sr = new StreamReader("countries_continents_codes_flags_url.csv"))
            {
                using (StreamWriter sw = new StreamWriter("Country_codes_and_flags.csv"))
                {
                    string[] columnNames = sr.ReadLine().Split(',');
                    sw.WriteLine(GetCorrectColumns(columnNames));

                    while (!sr.EndOfStream)
                    {
                        string[] originalValues = sr.ReadLine().Split(',');
                        string fileName = originalValues[1].Split('.')[0] + ".png";
                        if (File.Exists("flags/" + fileName) && !string.IsNullOrWhiteSpace(originalValues[4]))
                        {
                            originalValues[1] = fileName;
                            sw.WriteLine(GetCorrectColumns(originalValues));
                        }
                    }

                }

            }
        }

        private static string GetCorrectColumns(string[] values)
        {
            return $"{values[0]},{values[1]},{values[4]},{values[7]},{values[8]}";
        }

        private static void ScrapeFlagsFromNet()
        {
            using (StreamReader sr = new StreamReader("countries_continents_codes_flags_url.csv"))
            {
                sr.ReadLine();

                using (WebClient client = new WebClient())
                {
                    while (!sr.EndOfStream)
                    {
                        string[] values = sr.ReadLine().Split(',');
                        string url = values[2];
                        string filename = values[1];
                        try
                        {
                            client.DownloadFile(url, $"flags/{filename}");

                            var svgDocument = SvgDocument.Open($"flags/{filename}");
                            var bitmap = svgDocument.Draw();
                            bitmap.Save($"flags/{filename.Split('.')[0]}.png", ImageFormat.Png);

                            Thread.Sleep(2000);
                        }
                        catch (Exception e)
                        {
                            throw new Exception(e.Message);
                        }
                    }
                }

            }
        }
        #endregion
    }
}
