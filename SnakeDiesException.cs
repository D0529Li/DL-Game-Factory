namespace DL_Game_Project
{
    public class SnakeDiesException : Exception
    {
        private const string message = "Snake dies";
        public SnakeDiesException() : base(message + ".") { }
        public SnakeDiesException(string str) : base(message + " " + str) { }
    }
}
