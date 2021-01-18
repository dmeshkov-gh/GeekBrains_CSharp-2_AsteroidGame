using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTest
{
    abstract class Logger : ILogger
    {
        public abstract void Log(string Message);

        public void LogInformation(string Message)
        {
            Log($"{DateTime.Now:s}[Info]: {Message}");
        }

        public void LogWarning(string Message)
        {
            Log($"{DateTime.Now:s}[Warning]: {Message}");
        }

        public void LogError(string Message)
        {
            Log($"{DateTime.Now:s}[Error]: {Message}");
        }

        public void LogCritical(string Message)
        {
            Log($"{DateTime.Now:s}[Critical]: {Message}");
        }

        public abstract void Flush();
    }
}
