namespace DL_Game_Project
{
    public class CandyEventArgs : EventArgs
    {
        public SnakeModel Snake { get; }
        public CandyEventArgs(SnakeModel s) : base()
        {
            Snake = s;
        }
    }
}
