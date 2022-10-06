using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WeatherChecker.WebCrawler;

namespace WeatherChecker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class WeatherChecker : ControllerBase
    {
        private readonly ILogger<WeatherChecker> _logger;

        public WeatherChecker(ILogger<WeatherChecker> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> Get()
        {
            _logger.LogInformation("log Get");
            return StatusCode(StatusCodes.Status200OK, WeatherAustralianSite.GetWheatherInformation());
        }

        [HttpGet("{state}")]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> GetByState(string state)
        {
            if (!Enum.TryParse(state.ToUpper(), out WeatherAustralianSite.StateTerritory st))
                return BadRequest("Inválid State, please use: " + string.Join(", ", Enum.GetNames(typeof(WeatherAustralianSite.StateTerritory))));


            return StatusCode(StatusCodes.Status200OK, WeatherAustralianSite.GetWheatherInformation(st));
        }
    }
}
