﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsteroidGame.VisualObjects
{
    class Asteroid : CollisionObject
    {
        public int Power { get; set; } = 5;

        public Asteroid(Point Position, Point Direction, int Size) 
            : base(Position, Direction, new Size(Size, Size), Properties.Resources.Asteroid)
        {
        }

        public override void Update()
        {
            if (!Enabled) return;

            _Position.X += _Direction.X;
            _Position.Y += _Direction.Y;

            if (_Position.X < 0)
                _Direction.X *= -1;
            if (_Position.Y < 0)
                _Direction.Y *= -1;

            if (_Position.X > Game.Width - (_Size.Width * 2))
                _Direction.X *= -1;
            if (_Position.Y > Game.Height - (_Size.Height * 4))
                _Direction.Y *= -1;
        }

        public override void Reset()
        {
            _Position.X = Game.Width - _Size.Width * 2;
        }
        public void Reset(int Y)
        {
            _Position = new Point(Game.Width - 50, Y);
        }
    }
}