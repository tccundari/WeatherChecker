using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherChecker.WebCrawler;

namespace WeatherChecker.Test.API
{
    [TestFixture]
    public class WeatherCheckerAPI
    {
        private static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("jsconfig.json")
                .Build();
            return config;
        }

        private string BASE_URL { get
            {
                return InitConfiguration().GetSection("ApiUrl").Value;
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test GET API endpoint of WeatherChecker
        /// </summary>
        [Test]
        [TestCase("NSW")]
        [TestCase("TAS")]
        [TestCase("NONE")]
        [Parallelizable(ParallelScope.All)]
        public void WeatherCheckerTest(string state)
        {
            if (BASE_URL.Contains("localhost"))
                Assert.Pass();

            var urlEndPoint = BASE_URL + "WeatherChecker";

            if (!string.IsNullOrEmpty(state))
                urlEndPoint += "/" + state;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlEndPoint);
            request.Method = "GET";

            bool isAvailable;
            string error = string.Empty;

            try
            {
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    isAvailable = response.StatusCode == HttpStatusCode.OK;
                }
            }
            catch (WebException ex)
            {
                error = ex.ToString();
                isAvailable = false;
            }

            Assert.IsTrue(isAvailable, $"URL '{urlEndPoint}' is not available: {error}");
        }

        [Test]
        public void WeatherCheckerBadRequestTest()
        {
            if (BASE_URL.Contains("localhost"))
                Assert.Pass();

            var urlEndPoint = BASE_URL + "WeatherChecker/XXX";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = client.GetAsync(urlEndPoint).GetAwaiter().GetResult();

                Assert.AreEqual(HttpStatusCode.BadRequest, response.StatusCode, "API test expected a BadRequest (400) status code.");
            }
        }
    }
}
