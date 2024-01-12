using System.Timers;
using System.Windows;

namespace DL_Game_Factory
{
    public class Snake
    {
        public delegate void TimerDelegate(int a, int b);
        public const int GRID_SIZE = 15;
        public List<SnakePosition> BodyPositions = new();
        public Direction Direction { get; set; }
        private System.Timers.Timer timer;
        public SpeedOptions Speed { get; set; }

        public Snake()
        {
            BodyPositions.Add(new(0, 0));
            BodyPositions.Add(new(0, 1));
            BodyPositions.Add(new(0, 2));
            BodyPositions.Add(new(0, 3));

            Direction = Direction.right;

            timer = Speed switch
            {
                SpeedOptions.Slow => new System.Timers.Timer(500),
                SpeedOptions.Medium => new System.Timers.Timer(300),
                SpeedOptions.Fast => new System.Timers.Timer(100),
                _ => new System.Timers.Timer()
            };
        }

        public Snake(SpeedOptions speed) : this()
        {
            Speed = speed;
        }

        //public void ChangeSpeed(SpeedOptions speed)
        //{
        //    timer = Speed switch
        //    {
        //        SpeedOptions.Slow => new System.Timers.Timer(500),
        //        SpeedOptions.Medium => new System.Timers.Timer(300),
        //        SpeedOptions.Fast => new System.Timers.Timer(100),
        //        _ => new System.Timers.Timer()
        //    };
        //}

        public void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.left: if (direction != Direction.right) Direction = direction; break;
                case Direction.right: if (direction != Direction.left) Direction = direction; break;
                case Direction.up: if (direction != Direction.down) Direction = direction; break;
                case Direction.down: if (direction != Direction.up) Direction = direction; break;
                default: break;
            }
        }

        public void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Move();
        }

        public void Move()
        {
            try
            {
                BodyPositions.Add(BodyPositions[^1].Move(Direction, GRID_SIZE));
            }
            catch (SnakeDiesException e)
            {
                timer.Stop();
                MessageBox.Show(e.Message + "\nYour score is " + $"{BodyPositions.Count}");
                return;
            }
            BodyPositions.RemoveAt(0);
        }

        public void StartGame()
        {
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        public void PauseGame()
        {
            timer.Stop();
        }

        public void ResumeGame()
        {
            timer.Start();
        }

        public void StopGame()
        {
            timer.Stop();
            timer.Elapsed -= Timer_Elapsed;
            BodyPositions.Clear();
        }

        //private void SetBlack(int x, int y)
        //{
        //    _pos[x, y] = true;
        //    (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.Black);
        //}
        //private void SetEmpty(int x, int y)
        //{
        //    _pos[x, y] = false;
        //    if ((x + y) % 2 == 0) (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.LightGreen);
        //    else (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.Green);
        //}
        //private void SetYellow(int x, int y)
        //{
        //    (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.YellowGreen);
        //}
        //public void SetCandy(int x, int y)
        //{
        //    _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetYellow), new object[] { x, y });
        //}
        //public void RemoveCandy(int x, int y)
        //{
        //    _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetBlack), new object[] { x, y });
        //}
    }
}
