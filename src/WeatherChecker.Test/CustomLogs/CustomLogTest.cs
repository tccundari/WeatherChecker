using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.Logging;
using WeatherChecker.Logs;
using NUnit.Framework;
using Microsoft.Extensions.Options;

namespace WeatherChecker.Test.Logs
{
    [TestFixture]
    public class CustomLogTest
    {
        private CustomLog _logger;
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("jsconfig.json")
                .Build();
            return config;
        }

        [SetUp]
        public void Setup()
        {
            var customLoggerOptions = new CustomLogProviderOptions
            {
                FilePath = InitConfiguration().GetSection("Logging").GetSection("CustomLog").GetSection("Options").GetSection("FilePath").Value,
                FolderPath = InitConfiguration().GetSection("Logging").GetSection("CustomLog").GetSection("Options").GetSection("FolderPath").Value
            };
            var customLoggerProvider = new CustomLogProvider(Options.Create(customLoggerOptions));

            CustomLogFactory loggerFactoryCustom = new CustomLogFactory();

            _logger = loggerFactoryCustom.CreateCustomLogger(customLoggerProvider);
        }

        /// <summary>
        /// Simple Consult without parameter
        /// </summary>
        [Test]
        public void LogInformationTest()
        {
            var hash = Guid.NewGuid().ToString();

            _logger.LogInformation(string.Format("LogInformation GuiID {0}", hash));

            FileAssert.Exists(_logger.FilePath);

            StringAssert.Contains(hash, File.ReadAllText(_logger.FilePath));
        }
    }
}
