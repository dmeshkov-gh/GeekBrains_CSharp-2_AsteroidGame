using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class SpaceShip : ImageObject, ICollision
    {
        public event EventHandler Destroyed;
        public int Energy { get; set; } = 25;

        public Rectangle Rect => new Rectangle(_Position, _Size);

        public SpaceShip(Point Position, Point Direction, Size Size) 
            : base(Position, Direction, Size, Properties.Resources.SpaceShip)
        {
        }

        public override void Update() { }

        public bool CheckCollision(ICollision obj)
        {
            bool isCollided = Rect.IntersectsWith(obj.Rect);

            if(isCollided && obj is Asteroid asteroid)
            {
                ChangeEnergy(-asteroid.Power);
            }
            return isCollided;
        }

        private void ChangeEnergy(int delta)
        {
            Energy += delta;
            if (Energy <= 0)
            {
                Destroyed?.Invoke(this, EventArgs.Empty);
            }
        }

        public void MoveUp()
        {
            if (_Position.Y > 0)
                _Position.Y -= _Direction.Y;
        }
        public void MoveDown()
        {
            if (_Position.Y - _Size.Height < Game.Height)
                _Position.Y += _Direction.Y;
        }
    }
}
