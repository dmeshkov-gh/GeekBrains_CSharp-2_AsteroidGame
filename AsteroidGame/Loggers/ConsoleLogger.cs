using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroidGame.Loggers
{
    class ConsoleLogger : Logger
    {
        public override void Log(string Message)
        {
            Console.WriteLine(Message);
        }

        public override void Flush()
        {
            Console.WriteLine("All data has been saved.");
        }
    }
}
