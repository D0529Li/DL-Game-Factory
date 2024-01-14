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

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            var item = obj as Coordinate;

            return this == item;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public bool IsValid()
        {
            return X >= 0 && Y >= 0;
        }
    }
}
