using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherChecker.Logs
{
    public interface ILog
    {
        public void LogRegister(string message);
    }
}
