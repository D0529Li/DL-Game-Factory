namespace DL_Game_Project
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
