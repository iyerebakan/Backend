using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging
{
    public enum LogTypeEnum
    {
        Exception,
        HttpException,
        HttpRequestResponse,
        Database,
        Eventbus,
        Scheduler,
        Default,
    }
}
