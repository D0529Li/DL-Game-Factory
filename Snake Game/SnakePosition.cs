namespace DL_Game_Factory
{
    public class SnakePosition(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public SnakePosition Move(Direction direction, int gridSize)
        {
            int newX = X, newY = Y;
            switch (direction)
            {
                case Direction.Left:
                    if (Y == 0)
                        throw new SnakeDiesException(SnakeDiesReason.Snake_Hits_The_Wall);
                    newY--;
                    break;
                case Direction.Right:
                    if (Y == gridSize - 1)
                        throw new SnakeDiesException(SnakeDiesReason.Snake_Hits_The_Wall);
                    newY++;
                    break;
                case Direction.Up:
                    if (X == 0) throw new SnakeDiesException(SnakeDiesReason.Snake_Hits_The_Wall);
                    newX--;
                    break;
                case Direction.Down:
                    if (X == gridSize - 1)
                        throw new SnakeDiesException(SnakeDiesReason.Snake_Hits_The_Wall);
                    newX++;
                    break;
                default:
                    break;
            }
            return new SnakePosition(newX, newY);
        }
    }
}
