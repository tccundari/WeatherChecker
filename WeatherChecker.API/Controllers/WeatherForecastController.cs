using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using WeatherChecker.WebCrawler;

namespace WeatherChecker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class WeatherChecker : ControllerBase
    {
        private readonly ILogger<WeatherChecker> _logger;

        public WeatherChecker([NotNull]ILogger<WeatherChecker> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> Get()
        {
            try
            {
                _logger.LogInformation(3, "{0} GetWheatherInformation",DateTime.Now );

                var result = WeatherAustralianSite.GetWheatherInformation();

                _logger.LogTrace("Information aquired from site");

                return StatusCode(StatusCodes.Status200OK, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry about that =/");
            }
        }

        [HttpGet("{state}")]
        public ActionResult<IEnumerable<Entity.WeatherInfoPlaces>> GetByState(string state)
        {
            try
            {
                if (!Enum.TryParse(state.ToUpper(), out WeatherAustralianSite.StateTerritory st))
                    return BadRequest("Inválid State, please use: " + string.Join(", ", Enum.GetNames(typeof(WeatherAustralianSite.StateTerritory))));


                return StatusCode(StatusCodes.Status200OK, WeatherAustralianSite.GetWheatherInformation(st));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, "Sorry about that =/");
            }
        }
    }
}
