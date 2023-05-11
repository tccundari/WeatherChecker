using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using WeatherChecker.WebCrawler;

namespace WeatherChecker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("jsconfig.json", optional: true, reloadOnChange: true)
                .Build();

                //var log = new Logs.CustomLog(Configuration);

                //log.LogRegister("mensagem");

                var result = WeatherAustralianSite.GetWheatherInformation(WebCrawler.WeatherAustralianSite.StateTerritory.NSW);

                //Convert the object to Json string
                string json = JsonSerializer.Serialize(result, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping } );

                //saving the json string into a file
                File.WriteAllText(@"C:\Users\public\result.json", json);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
