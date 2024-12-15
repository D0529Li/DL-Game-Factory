namespace Snake_Game
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum SpeedOptions
    {
        Not_Selected,
        Slow,
        Medium,
        Fast
    }

    public enum SnakeDiesReason
    {
        Snake_Bites_Itself,
        Snake_Hits_The_Wall,
        Not_Defined
    }
}
