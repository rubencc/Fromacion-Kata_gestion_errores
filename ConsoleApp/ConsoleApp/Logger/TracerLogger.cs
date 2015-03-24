using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Logger
{
    public class TracerLogger : ILogger
    {
        public TracerLogger()
        {
            Debug.Listeners.Clear();
            Debug.Listeners.Add(new DefaultTraceListener());
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Trace.AutoFlush = true;
            Debug.AutoFlush = true;
        }

        public void WriteLogInfo(string message)
        {
            Trace.TraceInformation("Info:" + message);
        }

        public void WriteLogWarning(string message, string exceptionMessage)
        {
            Debug.Write("Warning: " + message + ", MessageException:" + exceptionMessage);
        }

        public void WriteLogWarning(string message)
        {
            Debug.Write("Warning: {0}", message);
        }

        public void WriteLogError(string message, string exceptionMessage)
        {
            Debug.Write("Error: " + message + ", MessageException:" + exceptionMessage);
        }
    }
}
