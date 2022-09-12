using System;

namespace WeatherChecker.Entity
{
    /// <summary>
    /// Generic Object to store weather info from the Australian Government Web Site
    /// </summary>
    public class WeatherInfoPlaces
    {
        public string DescriptionCity { get; set; }
        public string Min { get; set; }
        public string Max { get; set; }
        public string Precision { get; set; }
        public string ObsNow { get; set; }
        public string ObsLow { get; set; }
        public string ObsLowTime { get; set; }
        public string ObsHigh { get; set; }
        public string ObsHighTime { get; set; }
        public string ObsRain { get; set; }
    }
}
