using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    abstract class CollisionObject : ImageObject, ICollision
    {
        public event EventHandler Hit;
        protected CollisionObject(Point Position, Point Direction, Size Size, Image Image) 
            : base(Position, Direction, Size, Image)
        {
        }
        public Rectangle Rect => new Rectangle(_Position, _Size);

        public bool CheckCollision(ICollision obj)
        {
            if (Rect.IntersectsWith(obj.Rect))
            {
                Hit?.Invoke(this, EventArgs.Empty);
                return true;
            }
            else return false;
        }

        public abstract void Reset();
    }
}
