using System.Collections.Generic;
using System.Timers;
using System.Windows;

namespace DL_Game_Factory
{
    public class Snake
    {
        public delegate void SnakeMovedHandler(Coordinate oldPos, Coordinate newPos);
        public delegate void SnakeDiesHandler(SnakeDiesExceptions ex);
        public delegate void SnakeEatsCandyHandler(Candy oldCandy);
        public delegate void SnakeDirectionChangedHandler(Direction direction);
        public event SnakeMovedHandler? SnakeMoved;
        public event SnakeDiesHandler? SnakeDies;
        public event SnakeEatsCandyHandler? SnakeEatsCandy;
        public event SnakeDirectionChangedHandler? SnakeDirectionChanged;

        /// <summary>
        /// HEAD is the LAST element of the list.
        /// TAIL is the FIRST element of the list.
        /// </summary>
        private List<Coordinate> BodyPositions = new();
        public Direction Direction { get; set; }
        private System.Timers.Timer timer = new();
        public Candy Candy = new Candy(SnakeConstants.DEFAULT_GRID_SIZE);
        public SpeedOptions Speed { get; set; } = SpeedOptions.Not_Selected;

        public Snake() { }

        public List<Coordinate> GetBodyPositions() => BodyPositions;

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
            var newX = BodyPositions.Last().X;
            var newY = BodyPositions.Last().Y;
            switch (Direction)
            {
                case Direction.Left: newY--; break;
                case Direction.Right: newY++; break;
                case Direction.Up: newX--; break;
                case Direction.Down: newX++; break;
                default: break;
            }

            BodyPositions.Add(new(newX, newY));

            try
            {
                CheckIfSnakeDies();
            }
            catch (SnakeDiesExceptions ex)
            {
                // TBD: should throw instead of using event. should not show message box here.
                timer.Stop();
                MessageBox.Show(ex.Message + "\nYour score is " + $"{BodyPositions.Count}");
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

        private void CheckIfSnakeDies()
        {
            // Snake bites itself
            if (BodyPositions.Count != BodyPositions.Distinct().Count())
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Bites_Itself);

            // Snake hits the wall
            if (BodyPositions.Last().X < 0 || BodyPositions.Last().X >= SnakeConstants.DEFAULT_GRID_SIZE ||
                               BodyPositions.Last().Y < 0 || BodyPositions.Last().Y >= SnakeConstants.DEFAULT_GRID_SIZE)
                throw new SnakeDiesExceptions(SnakeDiesReason.Snake_Hits_The_Wall);
        }
    }
}
