namespace DL_Game_Factory
{
    public class SnakeDiesException : Exception
    {
        // SnakeDiesReason Reason { get; set; }
        public SnakeDiesException(SnakeDiesReason reason) : base("The snake dies because " + reason.ToString().Replace('_', ' ').ToLower() + ".")
        {
            // Reason = reason;
        }
        public SnakeDiesException() : this(SnakeDiesReason.Not_Defined) { }
    }
}
