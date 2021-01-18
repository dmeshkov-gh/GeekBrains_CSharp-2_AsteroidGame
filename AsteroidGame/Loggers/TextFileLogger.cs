using System;
using System.IO;

namespace AsteroidGame.Loggers
{
    class TextFileLogger
    {
        private readonly TextWriter _Writer;

        public TextFileLogger(string FileName)
        {
            _Writer = File.CreateText(FileName);
        }

        public void Log(string Message)
        {
            _Writer.WriteLine(Message);
            Flush();
        }

        private void Flush()
        {
            _Writer.Flush();
        }
    }
}
