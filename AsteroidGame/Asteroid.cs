using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    class Asteroid : VisualObject
    {
        public Asteroid(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {
        }

        public override void Draw(Graphics g)
        {
            Image asteriod = Image.FromFile("Asteroid.png");

            g.DrawImage(asteriod, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }
    }
}
