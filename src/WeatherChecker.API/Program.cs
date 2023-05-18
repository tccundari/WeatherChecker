using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using WeatherChecker.Logs;

namespace WeatherChecker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseKestrel(options =>
                {
                    options.ListenAnyIP(5000);
                    options.ListenAnyIP(5001, listenOptions =>
                    {
                        listenOptions.UseHttps(); // Use this line if you have a valid SSL certificate for local development
                    });
                });
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureLogging((context, logging) =>
            {
                logging.ClearProviders();

                logging.AddCustomLog(options =>
                {
                    context.Configuration.GetSection("Logging").GetSection("CustomLog").GetSection("Options").Bind(options);
                });
            });
    }
}
