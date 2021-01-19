using System;
using System.Windows.Forms;
using System.Drawing;

namespace AsteroidGame
{
    static class Game
    {
        private static BufferedGraphicsContext __Context;
        private static BufferedGraphics __Buffer;

        private static VisualObject[] __GameObjects;
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
            const int visual_objects_count = 40;
            __GameObjects = new VisualObject[visual_objects_count];

            for(int i = 0; i < visual_objects_count / 2; i++)
            {
                __GameObjects[i] = new Star(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-30, -15), 0),
                    10);
            }

            for (int i = visual_objects_count / 2; i < (visual_objects_count / 2) + (visual_objects_count / 4); i++)
            {
                __GameObjects[i] = new Asteroid(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-15, 15), random.Next(-10, 20)),
                    new Size(20, 20));
            }

            for (int i = (visual_objects_count / 2) + (visual_objects_count / 4); i < visual_objects_count; i++)
            {
                __GameObjects[i] = new Planet(
                    new Point(600, random.Next(0, Game.Width)),
                    new Point(random.Next(-10, -5), 0),
                    random.Next(10,70));
            }

        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;

            g.Clear(Color.Black);

            //g.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            //g.FillEllipse(Brushes.Red, new Rectangle(100, 100, 200, 200));

            foreach (var game_object in __GameObjects)
                game_object.Draw(g);

            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __GameObjects)
                game_object.Update();
        }
    }
}
