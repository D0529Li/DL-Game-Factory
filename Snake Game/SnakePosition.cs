namespace DL_Game_Factory
{
    public class SnakePosition(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public SnakePosition Move(Direction direction, int maxIndex)
        {
            int newX = X, newY = Y;
            switch (direction)
            {
                case Direction.left:
                    if (Y == 0) throw new SnakeDiesException();
                    newY--;
                    break;
                case Direction.right:
                    if (Y >= maxIndex) throw new SnakeDiesException();
                    newY++;
                    break;
                case Direction.up:
                    if (X == 0) throw new SnakeDiesException();
                    newX--;
                    break;
                case Direction.down:
                    if (X >= maxIndex) throw new SnakeDiesException();
                    newX++;
                    break;
                default:
                    break;
            }
            return new SnakePosition(newX, newY);
        }
    }
}
