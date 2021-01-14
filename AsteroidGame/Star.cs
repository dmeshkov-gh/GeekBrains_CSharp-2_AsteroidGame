using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame
{
    class Star : VisualObject
    {
        public Star(Point Position, Point Direction, int Size) 
            : base(Position, Direction, new Size(Size, Size))
        {
        }

        public override void Draw(Graphics g)
        {
            Image star = Image.FromFile("Star.jpg");

            g.DrawImage(star, _Position.X, _Position.Y, _Size.Width, _Size.Height);
            //g.DrawLine(new Pen(Color.Yellow, 2), 
            //    _Position.X, _Position.Y, 
            //    _Position.X + _Size.Width, _Position.Y + _Size.Width);

            //g.DrawLine(new Pen(Color.WhiteSmoke, 2), 
            //    _Position.X + _Size.Width, _Position.Y, 
            //    _Position.X, _Position.Y + _Size.Width);
        }

        public override void Update()
        {
            _Position.X += _Direction.X;

            if (_Position.X < 0)
                _Position.X = Game.Width + _Size.Width;

            //if (_Position.X > Game.Width /*+ _Size.Width*/)
            //    _Position.X = 0 - _Size;
        }
    }
}
 