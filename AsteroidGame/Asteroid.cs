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
        Image asteriod = Image.FromFile("Asteroid.png");
        public Asteroid(Point Position, Point Direction, Size Size) : base(Position, Direction, Size)
        {
        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(asteriod, _Position.X, _Position.Y, _Size.Width, _Size.Height);
        }
        public override void Update()
        {
            _Position.X += _Direction.X;
            _Position.Y += _Direction.Y;

            if (_Position.X < 0)
                _Direction.X *= -1;
            if (_Position.Y < 0)
                _Direction.Y *= -1;

            if (_Position.X > Game.Width - (_Size.Width * 2))
                _Direction.X *= -1;
            if (_Position.Y > Game.Height - (_Size.Height * 4))
                _Direction.Y *= -1;
        }
    }
}
