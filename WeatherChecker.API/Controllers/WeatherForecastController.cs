using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherChecker.WebCrawler;

namespace WeatherChecker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherChecker : ControllerBase
    {
        private readonly ILogger<WeatherChecker> _logger;
        private WeatherAustralianSite crawler = new WeatherAustralianSite();

        public WeatherChecker(ILogger<WeatherChecker> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> Get()
        {
            return StatusCode(StatusCodes.Status200OK, crawler.GetWheatherInformation());
        }

        [HttpGet("{state}")]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> GetByState(string state)
        {
            if (!Enum.TryParse(state.ToUpper(), out WeatherAustralianSite.StateTerritory st))
                return BadRequest("Inválid State, please use: " + string.Join(", ", Enum.GetNames(typeof(WeatherAustralianSite.StateTerritory))));


            return StatusCode(StatusCodes.Status200OK, crawler.GetWheatherInformation(st));
        }
    }
}
