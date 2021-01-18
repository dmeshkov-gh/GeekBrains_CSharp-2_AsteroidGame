using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    class GameObjectException : Exception
    {
        private string MessageDetails { get; set; }
        public DateTime ErrorTime { get; private set; }

        public GameObjectException(string message)
        {
            MessageDetails = message;
            ErrorTime = DateTime.Now;
        }

        public override string Message => $"[{ErrorTime}]: {MessageDetails}.";
    }
}
