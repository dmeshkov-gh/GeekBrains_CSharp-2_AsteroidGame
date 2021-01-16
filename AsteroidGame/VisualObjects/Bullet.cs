using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Bullet : CollisionObject
    {
        Random random = new Random();
        private const int __BulletSizeX = 25;
        private const int __BulletSizeY = 10;
        private const int __BulletSpeed = 3;
        public Bullet(int Position)
            : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX, __BulletSizeY), Properties.Resources.Bullet)
        {
        }

        //public override void Draw(Graphics g)
        //{
        //    var rect = Rect;
        //    g.FillEllipse(Brushes.Red, rect);
        //    g.DrawEllipse(Pens.White, rect);
        //}

        public override void Update() => _Position.X += __BulletSpeed;

        public override void Reset()
        {
            _Position.X = 0;
        }
    }
}
