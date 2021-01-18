using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleTest
{
    class TextFileLogger : Logger
    {
        private readonly TextWriter _Writer;

        public TextFileLogger(string FileName)
        {
            _Writer = File.CreateText(FileName);
            //((StreamWriter) _Writer).AutoFlush = true;
        }

        public override void Log(string Message)
        {
            _Writer.WriteLine(Message);
        }

        public override void Flush()
        {
            _Writer.Flush();
        }
    }
}
