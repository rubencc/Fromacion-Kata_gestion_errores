using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Logger
{
    public interface ILogger
    {
        void WriteLogInfo(string message);
        void WriteLogWarning(string message);
        void WriteLogWarning(string message, string exceptionMessage);
        void WriteLogError(string message, string exceptionMessage);
    }
}
