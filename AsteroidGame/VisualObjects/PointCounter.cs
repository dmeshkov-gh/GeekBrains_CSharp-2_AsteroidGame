using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class PointCounter : VisualObject
    {
        private static int _PointCounterWidth = 70;
        private static int _PointCounterHeight = 30;
        public int Points { get; set; } = 0;
        public PointCounter() 
            : base(new Point(0, 0), Point.Empty, new Size(_PointCounterWidth, _PointCounterHeight))
        {
        }

        public override void Draw(Graphics g)
        {
            g.DrawString($"POINTS: {Points}", new Font("Arial", 16), new SolidBrush(Color.White), _Position.X, _Position.Y);
        }

        public override void Update() 
        {
        }
    }
}
