using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Logs
{
    public static class CustomLogExtensions
    {
        public static ILoggingBuilder AddCustomLog(this ILoggingBuilder builder, Action<CustomLogProviderOptions> configure)
        {
            builder.Services.AddSingleton<ILoggerProvider, CustomLogProvider>();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
