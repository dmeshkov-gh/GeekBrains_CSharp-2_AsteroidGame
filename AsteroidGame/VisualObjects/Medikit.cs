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
        public Medikit(int Position, Point Direction, int Size, Image Image) 
            : base(new Point(Game.Width - _Medikit_Size, 0), Point.Empty, new Size(Size, Size), Properties.Resources.Medikit)
        {
        }

        public override void Reset()
        {
            _Position.X = Game.Width - _Medikit_Size;
        }

        public override void Update() => _Position.X += _Medikit_Speed;

        public void Heal(SpaceShip SpaceShip) => SpaceShip.Energy += 5;
    }
}
