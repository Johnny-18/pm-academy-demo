using System;
using System.Diagnostics;

namespace RequestProcessor.App.Logging
{
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            if(string.IsNullOrEmpty(message))
                return;
            
            Debug.WriteLine(message);
        }

        public void Log(Exception exception, string message)
        {
            if (exception == null || string.IsNullOrEmpty(message))
                return;
            
            Debug.WriteLine(message);
            Debug.WriteLine(exception);
        }
    }
}