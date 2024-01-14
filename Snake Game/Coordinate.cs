namespace DL_Game_Factory
{
    public class Coordinate(int x, int y)
    {
        public int X { get; set; } = x;
        public int Y { get; set; } = y;

        public static bool operator ==(Coordinate pos1, Coordinate pos2)
        {
            return pos1.X == pos2.X && pos1.Y == pos2.Y;
        }

        public static bool operator !=(Coordinate pos1, Coordinate pos2)
        {
            return pos1.X != pos2.X || pos1.Y != pos2.Y;
        }

        public bool IsValid()
        {
            return X >= 0 && Y >= 0;
        }
    }
}
