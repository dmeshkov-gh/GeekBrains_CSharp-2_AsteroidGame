using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;
using AsteroidGame.Loggers;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static int __Width;
        private static int __Height;

        private static VisualObject[] __GameObjects;
        private static Bullet __MyBullet;

        private static TextFileLogger __TextFileLogger = new TextFileLogger("Error_Report");

        public static int Width
        {
            get
            {
                return __Width;
            }
            set
            {
                __Width = value;
                if (value < 0 && value > 1000)
                    throw new ArgumentOutOfRangeException("Ширина больше 1000 или принимает отрицательное значение");
            }
        }
        public static int Height
        {
            get
            {
                return __Height;
            }
            set
            {
                __Height = value;
                if (value < 0 && value > 1000)
                    throw new ArgumentOutOfRangeException("Высота больше 1000 или принимает отрицательное значение");
            }
        }

        public static void Initialize(Form GameForm)
        {
            if (GameForm == null) throw new NullReferenceException("Game form has not been created");
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
                try
                {
                    SpaceObjects.Add(new Planet(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-10, -5), 0),
                    random.Next(30, 90)));
                }
                catch(GameObjectException e)
                {
                    __TextFileLogger.Log(e.Message);
                }

            }
            SpaceObjects.Add(__MyBullet = new Bullet(random.Next(50, Game.Height - 50)));
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
                    Asteroid myAsteroid = game_object as Asteroid;
                    if (myAsteroid.CheckCollision(__MyBullet))  //Проверяем сталкивается ли астероид с пулей
                    {           
                        System.Media.SystemSounds.Hand.Play();  //Если да, то издается звуковой сигнал, а объекты разводятся в разные стороны
                        __MyBullet.Reset();                 
                        myAsteroid.Reset();
                    }
                }
            }
        }
    }
}
