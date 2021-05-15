using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public class LoggingOptions
    {
        public LogLevel LogLevel { get; set; }

        public bool ConsoleEnabled { get; set; }

        public bool FileEnabled { get; set; }
    }
}
