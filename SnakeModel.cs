using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DL_Game_Project
{
    public class SnakeModel
    {
        public delegate void TimerDelegate(int a, int b);
        public static int maxIndex = 29;
        public System.Timers.Timer myTimer;
        private bool[,] _pos = new bool[maxIndex + 1, maxIndex + 1];

        public Queue<KeyValuePair<int, int>> BodyPositions = new ();



        public int HeadX { get; set; }
        public int HeadY { get; set; }
        public int TailX { get; set; }
        public int TailY { get; set; }
        public SpeedOptions Speed { get; set; }

        private Directions _headDirection;
        public Directions HeadDirection
        {
            get { return _headDirection; }
            set
            {
                switch (_headDirection)
                {
                    case Directions.left: if (value != Directions.right) _headDirection = value; break;
                    case Directions.right: if (value != Directions.left) _headDirection = value; break;
                    case Directions.up: if (value != Directions.down) _headDirection = value; break;
                    case Directions.down: if (value != Directions.up) _headDirection = value; break;
                    default: break;
                }
            }
        }
        private Grid _gameGrid;
        public Grid GameGrid { get { return _gameGrid; } }
        private Queue<Directions> _queueDirections = new Queue<Directions>();
        public int Length { get; set; }

        #region Commands

        public static RoutedCommand GoLeftCommand = new RoutedCommand("Go Left", typeof(SnakeModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Left) }));

        public static RoutedCommand GoRightCommand = new RoutedCommand("Go Right", typeof(SnakeModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Right) }));

        public static RoutedCommand GoUpCommand = new RoutedCommand("Go Up", typeof(SnakeModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Up) }));

        public static RoutedCommand GoDownCommand = new RoutedCommand("Go Down", typeof(SnakeModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Down) }));

        #endregion Commands

        public SnakeModel() : this(SpeedOptions.Slow) { }
        public SnakeModel(SpeedOptions speedSet)
        {
            BodyPositions.Enqueue(new(0, 0));
            BodyPositions.Enqueue(new(0, 1));
            BodyPositions.Enqueue(new(0, 2));
            BodyPositions.Enqueue(new(0, 3));
            Speed = speedSet;
            _headDirection = Directions.right;
            Length = 4;


            HeadX = 0;
            HeadY = 3;
            TailX = 0;
            TailY = 0;
            
            

            int timeGap;
            if (Speed == SpeedOptions.Slow) timeGap = 300;
            else if (Speed == SpeedOptions.Medium) timeGap = 200;
            else timeGap = 100;

            // Set Timer
            myTimer = new System.Timers.Timer(timeGap);
            for (int i = 0; i <= maxIndex; i++)
            {
                for (int j = 0; j <= maxIndex; j++)
                {
                    _pos[i, j] = false;
                }
            }

            // Set Game Grid
            _gameGrid = new Grid
            {
                Name = "gameGrid"
            };
            NameScope.SetNameScope(_gameGrid, new NameScope());
            for (int i = 0; i <= maxIndex; i++)
            {
                var newRow = new RowDefinition
                {
                    Height = new GridLength(14),
                    Name = $"gameGridRow{i}"
                };
                _gameGrid.RowDefinitions.Add(newRow);

                var newColumn = new ColumnDefinition
                {
                    Width = new GridLength(14),
                    Name = $"gameGridColumn{i}"
                };
                _gameGrid.ColumnDefinitions.Add(newColumn);
            }
            for (int x = 0; x <= maxIndex; x++)
            {
                for (int y = 0; y <= maxIndex; y++)
                {
                    Border border = new Border();
                    if ((x + y) % 2 == 0) border.Background = new SolidColorBrush(Colors.LightGreen);
                    else border.Background = new SolidColorBrush(Colors.Green);
                    _gameGrid.RegisterName($"gameBorderR{x}C{y}", border);
                    _gameGrid.Children.Add(border);
                    Grid.SetRow(border, x);
                    Grid.SetColumn(border, y);
                }
            }

            _pos[0, 0] = _pos[0, 1] = _pos[0, 2] = _pos[0, 3] = true;
            SetBlack(0, 0);
            SetBlack(0, 1);
            SetBlack(0, 2);
            SetBlack(0, 3);
            _queueDirections.Enqueue(Directions.right);
            _queueDirections.Enqueue(Directions.right);
            _queueDirections.Enqueue(Directions.right);
        }

        public void TimerBegin()
        {
            myTimer.Start();
        }

        public void TimerStop()
        {
            myTimer.Stop();
        }
        public void ChangeSpeed(SpeedOptions speed)
        {
            int timeGap;
            if (speed == SpeedOptions.Slow) timeGap = 600;
            else if (speed == SpeedOptions.Medium) timeGap = 400;
            else timeGap = 200;
            myTimer.Interval = timeGap;
        }
        public bool CheckSnakePos(int x, int y) => !_pos[x, y];
        public void ResetSnake()
        {
            _headDirection = Directions.right;
            HeadX = 0;
            HeadY = 3;
            TailX = 0;
            TailY = 0;
            Length = 4;
            while (_queueDirections.Count != 0) _queueDirections.Dequeue();
            for (int i = 0; i <= maxIndex; i++)
            {
                for (int j = 0; j <= maxIndex; j++)
                {
                    _pos[i, j] = false;
                }
            }
            _pos[0, 0] = _pos[0, 1] = _pos[0, 2] = _pos[0, 3] = true;
            for (int x = 0; x <= maxIndex; x++)
            {
                for (int y = 0; y <= maxIndex; y++)
                {
                    SetEmpty(x, y);
                }
            }
            SetBlack(0, 0);
            SetBlack(0, 1);
            SetBlack(0, 2);
            SetBlack(0, 3);
            _queueDirections.Enqueue(Directions.right);
            _queueDirections.Enqueue(Directions.right);
            _queueDirections.Enqueue(Directions.right);
        }
        private void SetBlack(int x, int y)
        {
            _pos[x, y] = true;
            (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.Black);
        }
        private void SetEmpty(int x, int y)
        {
            _pos[x, y] = false;
            if ((x + y) % 2 == 0) (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.LightGreen);
            else (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.Green);
        }
        private void SetYellow(int x, int y)
        {
            (_gameGrid.FindName($"gameBorderR{x}C{y}") as Border).Background = new SolidColorBrush(Colors.YellowGreen);
        }
        public void SetCandy(int x, int y)
        {
            _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetYellow), new object[] { x, y });
        }
        public void RemoveCandy(int x, int y)
        {
            _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetBlack), new object[] { x, y });
        }
        public bool Move()
        {
            MoveTail();
            return MoveHead();
        }
        public bool MoveHead()
        {
            try
            {
                CheckCollision();
            }
            catch (SnakeDiesException e)
            {
                TimerStop();
                MessageBox.Show(e.Message + "\nYour score is " + $"{Length}");
                return false;
            }

            if (_headDirection == Directions.left)
            {
                if (HeadY == 0) return false;
                _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetBlack), new object[] { HeadX, --HeadY });
            }
            else if (_headDirection == Directions.right)
            {
                if (HeadY >= maxIndex) return false;
                _gameGrid.Dispatcher.Invoke(new TimerDelegate(SetBlack), new object[] { HeadX, ++HeadY });
            }
            else if (_headDirection == Directions.up)
            {
                if (HeadX == 0) return false;
                _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetBlack), new object[] { --HeadX, HeadY });
            }
            else if (_headDirection == Directions.down)
            {
                if (HeadX >= maxIndex) return false;
                _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetBlack), new object[] { ++HeadX, HeadY });
            }
            _queueDirections.Enqueue(_headDirection);
            return true;
        }
        private void MoveTail()
        {
            Directions tailDirection = _queueDirections.Dequeue();
            _gameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SetEmpty), new object[] { TailX, TailY });
            if (tailDirection == Directions.left) TailY--;
            else if (tailDirection == Directions.right) TailY++;
            else if (tailDirection == Directions.up) TailX--;
            else if (tailDirection == Directions.down) TailX++;
        }
        private void CheckCollision()
        {
            int targetX = HeadX;
            int targetY = HeadY;
            switch (_headDirection)
            {
                case Directions.left: targetY--; break;
                case Directions.right: targetY++; break;
                case Directions.up: targetX--; break;
                case Directions.down: targetX++; break;
                default: break;
            }
            if (targetX < 0 || targetX > maxIndex || targetY < 0 || targetY > maxIndex)
                throw new SnakeDiesException("due to collision with wall");
            else if (!CheckSnakePos(targetX, targetY)) throw new SnakeDiesException("due to collision with itself.");
        }
    }
}
