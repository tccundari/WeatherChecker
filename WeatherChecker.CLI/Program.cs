using System;

namespace WeatherChecker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                WebCrawler.WeatherAustralianSite ObjCrawler = new WebCrawler.WeatherAustralianSite();

                Console.WriteLine("Wellcome to the WeatherChecker 0.1!");

                ObjCrawler.GetWheatherInformation();

                ObjCrawler.GetWheatherInformation(WebCrawler.WeatherAustralianSite.StateTerritory.NSW);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
