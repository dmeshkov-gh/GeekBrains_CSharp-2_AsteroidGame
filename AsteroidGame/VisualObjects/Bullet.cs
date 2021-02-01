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
        private const int __BulletSizeX = 25;
        private const int __BulletSizeY = 10;
        private const int __BulletSpeed = 15;
        public Bullet(int Position)
            : base(new Point(0, Position), Point.Empty, new Size(__BulletSizeX, __BulletSizeY), Properties.Resources.Fire)
        {
        }

        public override void Update()
        {
            if (!Enabled) return;

            _Position.X += __BulletSpeed;
        }

        public override void Reset()
        {
            _Position.X = 0;
        }
        public void Reset(int Y)
        {
            _Position = new Point(0, Y);
        }
    }
}
