using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Star : ImageObject
    {
        public Star(Point Position, Point Direction, int Size) 
            : base(Position, Direction, new Size(Size, Size), Properties.Resources.Star)
        {
        }

        public override void Update()
        {
            _Position.X += _Direction.X;

            if (_Position.X < 0)
                _Position.X = Game.Width + Size.Width;
        }
    }
}
 