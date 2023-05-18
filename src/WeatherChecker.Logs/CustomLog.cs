using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace WeatherChecker.Logs
{
    public class CustomLog : ILogger
    {
        private readonly CustomLogProvider _customLogProvider;

        public CustomLog([NotNull] CustomLogProvider provider)
        {
            this._customLogProvider = provider;
        }

        public IDisposable BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel != LogLevel.None;

            return false;
        }

        public string FilePath
        {
            get
            {
                return string.Format("{0}\\{1}", _customLogProvider.Options.FolderPath, _customLogProvider.Options.FilePath.Replace("{date}", DateTime.Now.ToString("yyyyMMdd")));
            }
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
                return;

            var fullFilePart = FilePath;

            var logRecord = string.Format("{0} [{1}] {2} {3}", DateTime.Now, logLevel.ToString(), formatter(state, exception), (exception != null ? exception.StackTrace : ""));

            Console.WriteLine(logRecord);

            using (var streamWriter = new StreamWriter(fullFilePart, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }
    }
}
