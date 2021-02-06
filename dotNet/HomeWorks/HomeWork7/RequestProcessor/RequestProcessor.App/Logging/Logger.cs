using System;
using System.Diagnostics;

namespace RequestProcessor.App.Logging
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Debug.WriteLine(message);
        }

        public void Log(Exception exception, string message)
        {
            Debug.WriteLine(message);
            Debug.WriteLine(exception);
        }
    }
}