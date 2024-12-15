namespace Snake_Game
{
    public class SnakeDiesExceptions : Exception
    {
        public SnakeDiesExceptions(SnakeDiesReason reason)
            : base("The snake dies because " + reason.ToString().Replace('_', ' ').ToLower() + ".") { }
        public SnakeDiesExceptions() : this(SnakeDiesReason.Not_Defined) { }
    }

    public class SnakeEatsCandyException : Exception
    {
        public SnakeEatsCandyException() { }
    }
}
