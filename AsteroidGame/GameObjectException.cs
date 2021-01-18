using System;
using System.IO;

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
