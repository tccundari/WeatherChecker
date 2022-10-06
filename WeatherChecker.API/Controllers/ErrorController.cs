using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherChecker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors]
    public class ErrorController : ControllerBase
    {
    }
}
