using System;
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
        }

        public override string Message => $"[{ErrorTime}]: {MessageDetails}. \nMember name of exception: {TargetSite}";
    }
}
