using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Medikit : CollisionObject
    {
        private static int _Medikit_Size = 15;
        private static int _Medikit_Speed = -15;
        public Medikit(int Position) 
            : base(new Point(Position, Position), Point.Empty, new Size(_Medikit_Size, _Medikit_Size), Properties.Resources.Medikit)
        {
        }

        public override void Reset()
        {
            _Position.X = Game.Width - _Medikit_Size;
        }

        public override void Update()
        {
            _Position.X += _Medikit_Speed;

            if (_Position.X < 0)
                _Position.X = Game.Width;
        }

        public void Heal(SpaceShip SpaceShip) => SpaceShip.Energy += 5;
    }
}
