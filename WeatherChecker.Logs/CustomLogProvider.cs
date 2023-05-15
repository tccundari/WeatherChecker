using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Logs
{
    [ProviderAlias("CustomLog")]
    public class CustomLogProvider : ILoggerProvider
    {
        public readonly CustomLogProviderOptions Options;

        public CustomLogProvider(IOptions<CustomLogProviderOptions> options)
        {
            Options = options.Value;

            if(!Directory.Exists(Options.FolderPath))
            {
                Directory.CreateDirectory(Options.FolderPath);
            }
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new CustomLog(this);
        }

        public CustomLog CreateLoggerCustomType()
        {
            return new CustomLog(this);
        }

        public void Dispose()
        {
        }
    }
}
