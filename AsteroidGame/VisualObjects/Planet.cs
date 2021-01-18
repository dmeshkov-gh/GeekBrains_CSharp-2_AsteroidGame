using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Planet : ImageObject
    {

        public Planet(Point Position, Point Direction, int Size)
            : base(Position, Direction, new Size(Size, Size), Properties.Resources.Planet)
        {
            if (Size > 80) throw new GameObjectException("Object has not been created. Planet size should not be more then 80");
        }

        protected override Size Size 
        { 
            get => base.Size;
            set
            {
                if (Size.Height < 30 || Size.Height > 70)
                    throw new GameObjectException("Planet size should no less than 10 and no more than 70");
                else
                    Size = base.Size;
            }
        }

        public override void Update()
        {
            _Position.X += _Direction.X;

            if (_Position.X < 0)
                _Position.X = Game.Width + Size.Width;
        }
    }
}
