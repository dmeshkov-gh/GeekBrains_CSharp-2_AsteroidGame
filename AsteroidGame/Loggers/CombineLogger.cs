using System;
using System.Collections.Generic;
using System.Text;

namespace AsteroidGame.Loggers
{
    class CombineLogger : Logger
    {
        private readonly Logger _Logger1;
        private readonly Logger _Logger2;

        public CombineLogger(Logger Logger1, Logger Logger2)
        {
            _Logger1 = Logger1;
            _Logger2 = Logger2;
        }

        public override void Log(string Message)
        {
            _Logger1.Log(Message);
            _Logger2.Log(Message);
        }

        public override void Flush()
        {
            _Logger1.Flush();
            _Logger2.Flush();
        }
    }
}
