using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherChecker.API.Logs
{
    public static class CustomLoggerExtensions
    {        
        public static ILoggingBuilder AddCustomLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, CustomLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <CustomLoggerConfiguration, CustomLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddColorConsoleLogger(
            this ILoggingBuilder builder,
            Action<CustomLoggerConfiguration> configure)
        {
            builder.AddCustomLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
