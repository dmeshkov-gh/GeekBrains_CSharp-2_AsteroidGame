using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using AsteroidGame.VisualObjects;
using AsteroidGame.Loggers;

namespace AsteroidGame
{
    delegate void LogRecorder(Logger Logger, LogType LogType, string Message); //Запись в журнал
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static int __Width;
        private static int __Height;

        private static VisualObject[] __GameObjects;
        private static Bullet __MyBullet;
        private static SpaceShip __MySpaceShip;
        private static Timer __Timer;

        private static Logger __TextFileLogger = new TextFileLogger("Report");
        private static Logger __ConsoleLogger = new ConsoleLogger();

        private static LogRecorder __LogRecorder;

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

            __Timer = new Timer { Interval = 100 };
            __Timer.Tick += OnTimerTick;
            __Timer.Start();

            GameForm.KeyDown += OnGameForm_KeyDown; 
        }

        private static void OnGameForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    __MyBullet = new Bullet(__MySpaceShip.Rect.Y);
                    break;

                case Keys.Up:
                    __MySpaceShip.MoveUp();
                    break;

                case Keys.Down:
                    __MySpaceShip.MoveDown();
                    break;
            }
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

            __LogRecorder = LogMessage; // Добавляем делегат, который будет записывать информацию в журнал

            for(int i = 0; i < 50; i++)
            {
                SpaceObjects.Add(new Star(
                    new Point(random.Next(0, Game.Width), random.Next(0, Game.Height)),
                    new Point(random.Next(-30, -15), 0),
                    10));
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object {SpaceObjects[i].GetType()} #{i+1} has been created");
            }

            for (int i = 0; i < 10; i++)
            {
                SpaceObjects.Add(new Asteroid(
                    new Point(random.Next(50, Game.Width - 50), random.Next(50, Game.Height - 50)),
                    new Point(-random.Next(0, 20), 20 - i),
                    20));
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object {SpaceObjects[i].GetType()} #{i + 1} has been created");
            }

            for (int i = 0; i < 5; i++)
            {
                try
                {
                    SpaceObjects.Add(new Planet(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-10, -5), 0),
                    random.Next(30, 90)));
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object {SpaceObjects[i].GetType()} #{i + 1} has been created");
                }
                catch(GameObjectException e)
                {
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, e.Message);
                }

            }
            __GameObjects = SpaceObjects.ToArray();

            __MyBullet = new Bullet(random.Next(50, Game.Height - 50));

            __MySpaceShip = new SpaceShip(
                new Point(0, random.Next(0, Game.Height - 30)),
                new Point(5, 5),
                new Size(30, 15));
            __MySpaceShip.Destroyed += OnShipDestroyed;
        }

        private static void OnShipDestroyed(object sender, EventArgs e)
        {
            __Timer.Stop();
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            g.DrawString("GAME OVER", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.White, 120, 200);
            __Buffer.Render();
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;

            g.Clear(Color.Black);

            foreach (var game_object in __GameObjects) //Отрисовываем объекты
                game_object?.Draw(g);

            __MyBullet?.Draw(g); //Рисуем пулю
            __MySpaceShip.Draw(g); //Рисуем корабль

            if (!__Timer.Enabled) return;
            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object?.Update();

            __MyBullet?.Update();

            for(int i = 0; i < __GameObjects.Length; i++)
            {
                var game_object = __GameObjects[i];

                if(game_object is ICollision collision_object)
                {
                    __MySpaceShip.CheckCollision(collision_object);

                    if (__MyBullet?.CheckCollision(collision_object) != true) continue;

                    __MyBullet = null;
                    __GameObjects[i] = null;
                    System.Media.SystemSounds.Hand.Play();
                }
            }
        }

        private static void LogMessage(Logger Logger, LogType LogType, string Message)
        {
            Logger.Log(LogType, Message);
        }
    }
}
