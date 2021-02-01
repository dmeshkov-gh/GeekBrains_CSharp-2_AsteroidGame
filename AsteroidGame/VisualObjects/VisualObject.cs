using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace AsteroidGame.VisualObjects
{
    abstract class VisualObject
    {
        protected Point _Position;
        protected Point _Direction;
        protected Size _Size;
        public bool Enabled { get; set; } = true;

        public VisualObject(Point Position, Point Direction, Size Size)
        {
            _Position = Position;
            _Direction = Direction;
            _Size = Size;
        }
        public abstract void Draw(Graphics g);

        public abstract void Update();
    }
}
