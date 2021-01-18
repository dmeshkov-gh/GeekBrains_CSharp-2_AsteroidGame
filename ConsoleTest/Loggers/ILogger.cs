using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
    interface ILogger
    {
        public void Log(string Message);

        public void LogInformation(string Message);

        public void LogWarning(string Message);

        public void LogError(string Message);

        public void LogCritical(string Message);

        public void Flush();
    }
}
