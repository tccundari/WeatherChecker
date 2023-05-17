using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using WeatherChecker.WebCrawler;
using Microsoft.Extensions.Logging;
using WeatherChecker.Logs;

namespace WeatherChecker.CLI
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Configure Logger
            IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("jsconfig.json", optional: true, reloadOnChange: true)
            .Build();

            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter("Microsoft", LogLevel.Warning)
                    .AddFilter("System", LogLevel.Warning)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Debug)
                    .AddCustomLog(options =>
                    {
                        Configuration.GetSection("Logging").GetSection("CustomLog").GetSection("Options").Bind(options);
                    });
            });

            ILogger logger = loggerFactory.CreateLogger<Program>();
            #endregion

            try
            {
                logger.LogInformation("Getting WeatherInformation");

                //Geeting Informationg 
                var result = WeatherAustralianSite.GetWheatherInformation();

                logger.LogInformation("Serializing info to file");

                //Convert the object to Json string
                string json = JsonSerializer.Serialize(result, new JsonSerializerOptions() { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping } );

                logger.LogInformation("Saving file");
                //saving the json string into a file
                File.WriteAllText(@"C:\Users\public\result.json", json);

                logger.LogInformation("End of Process");
            }
            catch(Exception ex)
            {
                logger.LogCritical(ex.ToString());
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
