using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WeatherChecker.WebCrawler
{
    /// <summary>
    /// Class used to navigate, search and extract weather information from the Australian government site
    /// </summary>
    public static class WeatherAustralianSite
    {
        /// <summary>
        /// Territories information for refined Searches
        /// </summary>
        public enum StateTerritory
        {
            NONE,
            NSW,
            VIC,
            QLD,
            WA,
            SA,
            TAS,
            ACT,
            NT
        }

        /// <summary>
        /// Base url to search weather information on Australian Government website
        /// </summary>
        private static string BGA_URL = "http://www.bom.gov.au/places/";

        /// <summary>
        /// Refer URL used to mimic browser navigation
        /// </summary>
        private static string BGA_REFER = "http://www.bom.gov.au/australia/index.shtml";

        /// <summary>
        /// The main method for get weather information
        /// </summary>
        /// <param name="state">The state territory name that will be used to raise precision on the search</param>
        public static IList<Entity.WeatherInfoPlaces> GetWheatherInformation(StateTerritory state = StateTerritory.NONE)
        {
            var url = BGA_URL;

            //if the optional parameter to state has soe value, then we consider it for the URL
            if (state != StateTerritory.NONE)
                url = url + state.ToString().ToLower() +"/";

            //Get all the html text from the request
            var html = Crawler.Get(url, BGA_REFER);

            //Extract only the html table where the weather info is
            var resultTable = GetMainResultTable(html);

            //Extract the info from the table and fill a list with our generic object
            return GetWeatherInfo(resultTable);
        }

        /// <summary>
        /// Extract only the html related to weather information
        /// </summary>
        /// <param name="html">Entire html page fromm request</param>
        /// <returns>html containing the table of weather information</returns>
        private static string GetMainResultTable(string html)
        {
            var mtTable = Regex.Match(html, "<tbody>(.*?)<\\/tbody>", RegexOptions.Singleline);

            if (!mtTable.Success)
                return string.Empty;

            return mtTable.Groups[1].Value;
        }

        /// <summary>
        /// Extract all the weather information based on given html table content from the Australian government site
        /// </summary>
        /// <param name="table">html table with the weather information from the page</param>
        /// <returns>List of generic object filled with all weather information from the extracted table</returns>
        private static List<Entity.WeatherInfoPlaces> GetWeatherInfo(string table)
        {
            //Generic list to store all the weather information
            var lstInfos = new List<Entity.WeatherInfoPlaces>();

            //Regex patter used to read elements from the table
            string pattern = string.Empty;

            //I realize that table content can change depend of the search, if you pass the state information yout will get the columns for lower and highest temperature
            if (table.Contains("obs rain"))
                pattern = "<td class=\"description\"><a href=\".*?\" class=\".*?\">(.*?)<\\/a><\\/td><td class=\"min\".*?>(.*?)<.*?td class=\"max\">(.*?)<\\/td><td class=\"precis\">(.*?)<\\/td><td class=\"obs now\">(.*?)<\\/td><td class=\"obs rain\">(.*?)<\\/td><\\/tr>";
            else
                pattern = "<td class=\"description\"><a href=\".*?\" class=\".*?\">(.*?)<\\/a><\\/td><td class=\"min\".*?>(.*?)<.*?td class=\"max\">(.*?)<\\/td><td class=\"precis\">(.*?)<\\/td><td class=\"obs now\">(.*?)<\\/td><td class=\"obs low\">(.*?)<span class=\"time\"> \\((.*?)\\)<\\/span><\\/td><td class=\"obs high\">(.*?)<span class=\"time\"> \\((.*?)\\)<\\/span><\\/td><\\/tr>";

            //must use the option singleline to read html with brealines
            var mtInfos = Regex.Matches(table, pattern, RegexOptions.Singleline);

            if (mtInfos.Count < 1)
                return lstInfos;

            foreach(Match mt in mtInfos)
            {
                var info = new Entity.WeatherInfoPlaces();

                info.DescriptionCity = mt.Groups[1].Value;
                info.Min = WebUtility.HtmlDecode(mt.Groups[2].Value);
                info.Max = WebUtility.HtmlDecode(mt.Groups[3].Value);
                info.Precision = mt.Groups[4].Value;
                info.ObsNow = WebUtility.HtmlDecode(mt.Groups[5].Value);

                //check if table have six or more columns, it is one regex patter for each
                if (mt.Groups.Count <= 7)
                    info.ObsRain = mt.Groups[6].Value;
                else
                {
                    info.ObsLow = WebUtility.HtmlDecode(mt.Groups[6].Value);
                    info.ObsLowTime = WebUtility.HtmlDecode(mt.Groups[7].Value);
                    info.ObsHigh = WebUtility.HtmlDecode(mt.Groups[8].Value);
                    info.ObsHighTime = WebUtility.HtmlDecode(mt.Groups[9].Value);
                }

                lstInfos.Add(info);
            }

            return lstInfos;
        }
    }
}
