using System.Timers;
using System.Windows;

namespace DL_Game_Project
{
    public class Snake
    {
        public delegate void TimerDelegate(int a, int b);
        public int maxIndex = 15;
        public List<SnakePosition> BodyPositions = new();
        public Directions Direction { get; set; }
        private System.Timers.Timer timer;
        public SpeedOptions Speed { get; set; }

        public Snake()
        {
            BodyPositions.Add(new(0, 0));
            BodyPositions.Add(new(0, 1));
            BodyPositions.Add(new(0, 2));
            BodyPositions.Add(new(0, 3));

            Direction = Directions.right;

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

        public void ChangeDirection(Directions direction)
        {
            switch (direction)
            {
                case Directions.left: if (direction != Directions.right) Direction = direction; break;
                case Directions.right: if (direction != Directions.left) Direction = direction; break;
                case Directions.up: if (direction != Directions.down) Direction = direction; break;
                case Directions.down: if (direction != Directions.up) Direction = direction; break;
                default: break;
            }
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Move();
        }

        public void Move()
        {
            try
            {
                BodyPositions.Add(BodyPositions[^1].Move(Direction, maxIndex));
            }
            catch (SnakeDiesException e)
            {
                timer.Stop();
                MessageBox.Show(e.Message + "\nYour score is " + $"{BodyPositions.Count}");
                return;
            }
            BodyPositions.RemoveAt(0);
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
