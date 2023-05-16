using NUnit.Framework;

namespace WeatherChecker.WebCrawler.Test
{
    [TestFixture]
    public class WeatherAustralianSiteTest
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Simple Consult without parameter
        /// </summary>
        [Test]
        public void GetWheatherInformationTest()
        {
            var sut = WeatherAustralianSite.GetWheatherInformation();

            Assert.IsNotNull(sut);
            Assert.That(sut.Count > 0);
        }

        /// <summary>
        /// Consult with several Parameters
        /// </summary>
        /// <param name="stateTerritory"></param>
        [Test]
        [TestCase(WeatherAustralianSite.StateTerritory.NSW)]
        [TestCase(WeatherAustralianSite.StateTerritory.TAS)]
        [TestCase(WeatherAustralianSite.StateTerritory.NONE)]
        [Parallelizable(ParallelScope.All)]
        public void GetWheatherInformationTest(WeatherAustralianSite.StateTerritory stateTerritory)
        {
            var sut = WeatherAustralianSite.GetWheatherInformation(stateTerritory);

            Assert.IsNotNull(sut);
            Assert.That(sut.Count > 0);
        }
    }
}