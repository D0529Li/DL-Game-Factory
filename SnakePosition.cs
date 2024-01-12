namespace DL_Game_Project
{
    public class SnakePosition(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public SnakePosition Move(Directions direction, int maxIndex)
        {
            int newX = X, newY = Y;
            switch (direction)
            {
                case Directions.left:
                    if (Y == 0) throw new SnakeDiesException();
                    newY--;
                    break;
                case Directions.right:
                    if (Y >= maxIndex) throw new SnakeDiesException();
                    newY++;
                    break;
                case Directions.up:
                    if (X == 0) throw new SnakeDiesException();
                    newX--;
                    break;
                case Directions.down:
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
