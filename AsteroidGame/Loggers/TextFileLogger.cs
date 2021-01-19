using System;
using System.IO;

namespace AsteroidGame.Loggers
{
    class TextFileLogger : Logger
    {
        private readonly TextWriter _Writer;

        public TextFileLogger(string FileName)
        {
            _Writer = File.CreateText(FileName);
        }

        public override void Log(string Message)
        {
            _Writer.WriteLine(Message);
            Flush();
        }

        public override void Flush()
        {
            _Writer.Flush();
        }
    }
}
