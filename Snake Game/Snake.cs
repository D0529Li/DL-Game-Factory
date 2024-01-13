using DL_Game_Factory;
using System.Timers;
using System.Windows;

namespace DL_Game_Factory
{
    public class Snake
    {
        public delegate void SnakeMovedHandler(SnakePosition oldPos, SnakePosition newPos);
        public delegate void SnakeDirectionChangedHandler(Direction direction);
        public event SnakeMovedHandler? SnakeMoved;
        public event SnakeDirectionChangedHandler? SnakeDirectionChanged;
        private List<SnakePosition> BodyPositions = new();
        public Direction Direction { get; set; }
        private System.Timers.Timer timer = new();
        private Candy candy = new Candy(SnakeConstants.DEFAULT_GRID_SIZE);
        public SpeedOptions Speed { get; set; } = SpeedOptions.Not_Selected;

        public Snake() { }

        public List<SnakePosition> GetBodyPositions() => BodyPositions;

        public void ChangeDirection(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Left: if (Direction != Direction.Right) Direction = newDirection; break;
                case Direction.Right: if (Direction != Direction.Left) Direction = newDirection; break;
                case Direction.Up: if (Direction != Direction.Down) Direction = newDirection; break;
                case Direction.Down: if (Direction != Direction.Up) Direction = newDirection; break;
                default: return;
            }
            SnakeDirectionChanged?.Invoke(Direction);
        }

        public void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Move();
        }

        public void Move()
        {
            SnakePosition newPos, oldPos;
            try
            {
                newPos = BodyPositions[^1].Move(Direction, SnakeConstants.DEFAULT_GRID_SIZE);
                oldPos = BodyPositions.First();
                BodyPositions.Add(BodyPositions[^1].Move(Direction, SnakeConstants.DEFAULT_GRID_SIZE));
            }
            catch (SnakeDiesExceptions e)
            {
                timer.Stop();
                MessageBox.Show(e.Message + "\nYour score is " + $"{BodyPositions.Count}");
                return;
            }
            BodyPositions.RemoveAt(0);

            SnakeMoved?.Invoke(oldPos, newPos);
        }

        public void Initialize(SpeedOptions speed)
        {
            BodyPositions.Add(new(0, 0));
            BodyPositions.Add(new(0, 1));
            BodyPositions.Add(new(0, 2));
            BodyPositions.Add(new(0, 3));

            Direction = Direction.Right;
            Speed = speed;

            timer.Interval = Speed switch
            {
                SpeedOptions.Slow => 1500,
                SpeedOptions.Medium => 1000,
                SpeedOptions.Fast => 500,
                _ => 100
            };
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

        private void Check()
        {
            // Snake bites itself
            if (BodyPositions.Count != BodyPositions.Distinct().Count())
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Bites_Itself);

            // Snake hits the wall

            // Snake eats candy
            if (BodyPositions.Last() == new SnakePosition(candy.X, candy.Y))
            {
                candy.GenerateCandy();
                throw new SnakeEatsCandyException();
            }
        }

        private void CheckIfSnakeEatsItself()
        {
            if (BodyPositions.Count != BodyPositions.Distinct().Count())
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Bites_Itself);
        }
    }
}
