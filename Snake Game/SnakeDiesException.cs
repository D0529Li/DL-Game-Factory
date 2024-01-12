namespace DL_Game_Factory
{
    public class SnakeDiesException : Exception
    {
        private const string message = "Snake dies";
        public SnakeDiesException() : base(message + ".") { }
        public SnakeDiesException(string str) : base(message + " " + str) { }
    }
}
