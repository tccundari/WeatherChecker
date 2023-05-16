using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Logs
{
    public class CustomLogFactory : LoggerFactory
    {
        public CustomLog CreateCustomLogger(CustomLogProvider provider)
        {
            return new CustomLog(provider);
        }

    }
}
