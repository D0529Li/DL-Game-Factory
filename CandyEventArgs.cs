namespace Snake_Game
{
    public class CandyEventArgs : EventArgs
    {
        public Snake Snake { get; }
        public CandyEventArgs(Snake s) : base()
        {
            Snake = s;
        }
    }
}
