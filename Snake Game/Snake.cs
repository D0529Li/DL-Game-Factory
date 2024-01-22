using System.Timers;

namespace DL_Game_Factory
{
    public class Snake
    {
        #region Custom Events

        public delegate void SnakeMovedHandler(Coordinate oldPos, Coordinate newPos);
        public delegate void SnakeDiesHandler(SnakeDiesExceptions ex);
        public delegate void SnakeEatsCandyHandler(Candy oldCandy);
        public delegate void SnakeDirectionChangedHandler(Direction direction);
        public event SnakeMovedHandler? SnakeMoved;
        public event SnakeDiesHandler? SnakeDies;
        public event SnakeEatsCandyHandler? SnakeEatsCandy;
        public event SnakeDirectionChangedHandler? SnakeDirectionChanged;

        #endregion Custom Events

        /// <summary>
        /// HEAD is the LAST element of the list.
        /// TAIL is the FIRST element of the list.
        /// </summary>
        private readonly List<Coordinate> BodyPositions = new();

        private readonly System.Timers.Timer timer = new();

        public Direction Direction { get; set; }
        public Direction BufferedDirection { get; set; }
        
        public Candy Candy = new Candy(SnakeConstants.DEFAULT_GRID_SIZE);
        public SpeedOptions Speed { get; set; } = SpeedOptions.Not_Selected;

        public Snake() { }

        public List<Coordinate> GetBodyPositions() => BodyPositions;


        public void BufferDirection(Direction newDirection)
        {
            switch (newDirection)
            {
                case Direction.Left: if (Direction != Direction.Right) BufferedDirection = newDirection; break;
                case Direction.Right: if (Direction != Direction.Left) BufferedDirection = newDirection; break;
                case Direction.Up: if (Direction != Direction.Down) BufferedDirection = newDirection; break;
                case Direction.Down: if (Direction != Direction.Up) BufferedDirection = newDirection; break;
                default: return;
            }
        }

        public void Timer_Elapsed(object? sender, ElapsedEventArgs e)
        {
            Move();
        }        

        public void Initialize(SpeedOptions speed)
        {
            BodyPositions.Add(new(0, 0));
            BodyPositions.Add(new(0, 1));
            BodyPositions.Add(new(0, 2));
            BodyPositions.Add(new(0, 3));

            BufferedDirection = Direction = Direction.Right;
            Speed = speed;

            timer.Interval = Speed switch
            {
                SpeedOptions.Slow => 1500,
                SpeedOptions.Medium => 1000,
                SpeedOptions.Fast => 500,
                _ => 100
            };

            Candy.GenerateCandy(BodyPositions);
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

        public void Move()
        {
            var newX = BodyPositions.Last().X;
            var newY = BodyPositions.Last().Y;

            Direction = BufferedDirection;
            switch (Direction)
            {
                case Direction.Left: newY--; break;
                case Direction.Right: newY++; break;
                case Direction.Up: newX--; break;
                case Direction.Down: newX++; break;
                default: break;
            }
            SnakeDirectionChanged?.Invoke(Direction);

            BodyPositions.Add(new(newX, newY));

            try
            {
                CheckIfSnakeDies();
            }
            catch (SnakeDiesExceptions ex)
            {
                timer.Stop();
                SnakeDies?.Invoke(ex);
                return;
            }

            // Check if snake eats candy
            var candyEaten = false;
            if (BodyPositions.Last() == Candy.Coordinate)
            {
                candyEaten = true;
                var oldCandy = new Candy(Candy.Coordinate.X, Candy.Coordinate.Y);
                Candy.GenerateCandy(BodyPositions);
                SnakeEatsCandy?.Invoke(oldCandy);
            }
            if (!candyEaten)
            {
                SnakeMoved?.Invoke(BodyPositions.First(), new(newX, newY));
                BodyPositions.RemoveAt(0);
            }
        }

        private void CheckIfSnakeDies()
        {
            // Snake bites itself
            if (BodyPositions.Count != BodyPositions.Distinct().Count())
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Bites_Itself);

            // Snake hits wall
            if (BodyPositions.Last().X < 0 || BodyPositions.Last().X >= SnakeConstants.DEFAULT_GRID_SIZE ||
                               BodyPositions.Last().Y < 0 || BodyPositions.Last().Y >= SnakeConstants.DEFAULT_GRID_SIZE)
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Hits_The_Wall);
        }
    }
}
