using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
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

        private static Random random = new Random();

        private static List<VisualObject> __VisualObjects = new();
        private static int __NumberOfAsteroids = 3;
        private static List<Asteroid> __Asteroids = new List<Asteroid>(__NumberOfAsteroids);
        private static List<Bullet> __Bullets = new();
        private static List<Medikit> __Medikits = new();
        private static SpaceShip __SpaceShip;
        private static PointCounter __PointCounter;
        private static Timer __Timer;

        private static Logger __TextFileLogger = new TextFileLogger("Report");
        private static Logger __ConsoleLogger = new ConsoleLogger();
        private static Logger __CombineLogger = new CombineLogger(__ConsoleLogger, __TextFileLogger); // Комбинированный логгер - в консоль и в файл

        private static LogRecorder __LogRecorder = LogMessage; // Добавляем делегат, который будет записывать информацию в журнал

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
                {
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogCritical, "An attempt to create form with width less than zero or more than 1000!");
                    throw new ArgumentOutOfRangeException("Width can't be more than 1000 or less than 0");
                }
                    
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
                {
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogCritical, "An attempt to create form with height less than zero or more than 1000!");
                    throw new ArgumentOutOfRangeException("Height can't be more than 1000 or less than 0");
                }   
            }
        }

        public static void Initialize(Form GameForm)
        {
            if (GameForm == null) 
            {
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogCritical, "Form does not exist!");
                throw new NullReferenceException("Game form has not been created");
            }
            
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
                    var new_bullet = new Bullet(__SpaceShip.Rect.Y);
                    new_bullet.Hit += OnHit; // Добавляем пуле обработчик события при столкновении с астероидом

                    var disabled_bullet = __Bullets.FirstOrDefault(b => !b.Enabled);
                    if (disabled_bullet != null)
                    {
                        disabled_bullet.Enabled = true;
                        disabled_bullet.Reset(__SpaceShip.Rect.Y);
                    }
                    else
                        __Bullets.Add(new_bullet);
                    break;

                case Keys.Up:
                    __SpaceShip.MoveUp();
                    break;

                case Keys.Down:
                    __SpaceShip.MoveDown();
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
            

            for(int i = 0; i < 50; i++) // Загружаем звёзды
            {
                __VisualObjects.Add(new Star(
                    new Point(random.Next(0, Width), random.Next(0, Height)),
                    new Point(random.Next(-30, -15), 0),
                    10));
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object Star #{i + 1} has been created");
            }

            for (int i = 0; i < 5; i++) // Загружаем планеты
            {
                try
                {
                    __VisualObjects.Add(new Planet(
                    new Point(600, random.Next(0, Width)),
                    new Point(random.Next(-10, -5), 0),
                    random.Next(30, 90)));
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object Planet #{i + 1} has been created");
                }
                catch(GameObjectException e)
                {
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogWarning, e.Message);
                }

            }

            LoadAsteroids(__NumberOfAsteroids);

            for (int i = 0; i < 3; i++) // Загружаем аптечки
            {
                __Medikits.Add(new Medikit(random.Next(50, Height - 50)));
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object Medikit #{i + 1} has been created");
            }

            __Bullets.Clear(); // Убираем пули с экрана, если есть

            __SpaceShip = new SpaceShip(                        //Создаем корабль
                new Point(0, random.Next(0, Height - 30)),
                new Point(5, 5),
                new Size(30, 15));
            __SpaceShip.Destroyed += OnShipDestroyed;

            __PointCounter = new PointCounter(); // Создаем объект для отображения сбитых астероидов и оставшейся энергии корабля
        }

        private static void LoadAsteroids(int Number)
        {
            for (int i = 0; i < Number; i++) // Загружаем астероиды
            {
                var disables_asteroid = __Asteroids.FirstOrDefault(a => !a.Enabled);

                if(disables_asteroid != null)
                {
                    disables_asteroid.Enabled = true;
                    disables_asteroid.Reset(random.Next(50, Height - 50));
                }
                else
                {
                    __Asteroids.Add(new Asteroid(
                    new Point(random.Next(200, Width - 50), random.Next(50, Height - 50)),
                    new Point(-random.Next(0, 20), 20 - i),
                    20));
                }

                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Object Asteroid #{i + 1} has been created");
            }
        }

        private static void OnShipDestroyed(object sender, EventArgs e)
        {
            __Timer.Stop();
            Graphics g = __Buffer.Graphics;
            g.Clear(Color.Black);
            g.DrawString("GAME OVER", new Font(FontFamily.GenericSerif, 60, FontStyle.Bold), Brushes.White, 120, 200);
            __Buffer.Render();

            __LogRecorder?.Invoke(__TextFileLogger, LogType.LogCritical, $"Spaceship got destroyed. Shapeship energy {__SpaceShip.Energy}."); //Запись в журнал при разрушении корабля
        }

        public static void Draw()
        {
            Graphics g = __Buffer.Graphics;

            g.Clear(Color.Black);

            foreach (var game_object in __VisualObjects) //Отрисовываем объекты, не участвующие в игре
                game_object?.Draw(g);

            foreach (var asteroid in __Asteroids) //Рисуем игровые объекты - астероиды, пули, корабль, аптечки
                asteroid?.Draw(g);

            foreach (var medikit in __Medikits)
                medikit.Draw(g);

            __Bullets.ForEach(bullet => bullet.Draw(g)); 
      
            __SpaceShip.Draw(g); 

            __PointCounter.Draw(g, __SpaceShip); // Рисуем информационное поле

            if (!__Timer.Enabled) return;
            __Buffer.Render();
        }

        private static void Update()
        {
            foreach (var game_object in __VisualObjects)
                game_object?.Update();

            foreach (var asteroid in __Asteroids)
                asteroid?.Update();

            foreach (var medikit in __Medikits)
                medikit?.Update();

            __Bullets?.ForEach(bullet => bullet.Update());

            foreach(var asteroid in __Asteroids.Where(a => a.Enabled).OfType<ICollision>())
            {
                if (__SpaceShip.CheckCollision(asteroid))
                {
                    __LogRecorder?.Invoke(__TextFileLogger, LogType.LogWarning,
                        $"Spaceship got {((Asteroid)asteroid).Power} damage. Shapeship energy {__SpaceShip.Energy}.");

                    ((VisualObject)asteroid).Enabled = false;
                    continue;
                }

                foreach(var bullet in __Bullets.Where(b => b.Enabled))
                {
                    if (!bullet.CheckCollision(asteroid)) continue;
                    System.Media.SystemSounds.Hand.Play();
                    ((VisualObject)asteroid).Enabled = false;
                    bullet.Enabled = false;
                }
            }

            if (__Asteroids.All(a => !a.Enabled)) // Проверяем остались ли астероиды в игре
            {
                __NumberOfAsteroids++;
                LoadAsteroids(__NumberOfAsteroids);
            }  

            foreach(var medikit in __Medikits.Where(m => m.Enabled && __SpaceShip.CheckCollision(m)))
            {
                medikit?.Heal(__SpaceShip);
                medikit.Enabled = false;
                __LogRecorder?.Invoke(__TextFileLogger, LogType.LogInformation, $"Spaceship got healed. Shapeship energy {__SpaceShip.Energy}.");
            }

            foreach (var bullet in __Bullets.Where(b => b.Enabled && b.Rect.X > Width))
                bullet.Enabled = false;

        }

        private static void OnHit(object sender, EventArgs e)
        {
            __PointCounter.Points++;
        }

        private static void LogMessage(Logger Logger, LogType LogType, string Message)
        {
            Logger.Log(LogType, Message);
        }
    }
}
