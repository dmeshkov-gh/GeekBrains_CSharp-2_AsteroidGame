using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroidGame.Loggers
{
    enum LogType
    {
        LogInformation,
        LogWarning,
        LogError,
        LogCritical
    }
    abstract class Logger : ILogger
    {
        public void Log(LogType LogType, string Message)
        {
            switch (LogType)
            {
                case LogType.LogInformation:
                    LogInformation(Message);
                    break;
                case LogType.LogWarning:
                    LogWarning(Message);
                    break;
                case LogType.LogError:
                    LogError(Message);
                    break;
                case LogType.LogCritical:
                    LogCritical(Message);
                    break;
            }
        }

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
