using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __GameObjects;
        private static Bullet __MyBullet = new Bullet(300);
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static void Initialize(Form GameForm)
        {
            Width = GameForm.Width;
            Height = GameForm.Height;

            __Context = BufferedGraphicsManager.Current;
            Graphics g = GameForm.CreateGraphics();
            __Buffer = __Context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Timer timer = new Timer { Interval = 100 };
            timer.Tick += OnTimerTick;
            timer.Start();
        }
        private static void OnTimerTick(object sender, EventArgs e)
        {
            Update();
            Draw();
        }

        public static void Load()
        {
            Random random = new Random();
            List<VisualObject> SpaceObjects = new List<VisualObject>();

            for(int i = 0; i < 50; i++)
            {
                SpaceObjects.Add(new Star(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-30, -15), 0),
                    10));
            }

            for (int i = 0; i < 10; i++)
            {
                SpaceObjects.Add(new Asteroid(
                    new Point(600, i * 20),
                    new Point(15 - i, 20 - i),
                    20));
            }

            for (int i = 0; i < 5; i++)
            {
                SpaceObjects.Add(new Planet(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-10, -5), 0),
                    random.Next(40,70)));
            }
            SpaceObjects.Add(__MyBullet);
            __GameObjects = SpaceObjects.ToArray();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;

            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects)
                game_object.Draw(g);

            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
            {
                game_object.Update();
                if(game_object is Asteroid)
                {
                    ICollision myAsteroid = game_object as Asteroid;
                    if (myAsteroid.CheckCollision(__MyBullet)) { System.Media.SystemSounds.Hand.Play(); }
                }
            }
        }
    }
}
