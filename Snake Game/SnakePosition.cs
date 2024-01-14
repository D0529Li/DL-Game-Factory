namespace DL_Game_Factory
{
    public class SnakePosition(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public static bool operator ==(SnakePosition pos1, SnakePosition pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public static bool operator !=(SnakePosition pos1, SnakePosition pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }
    }
}
