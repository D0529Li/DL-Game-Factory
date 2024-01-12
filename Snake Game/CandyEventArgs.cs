namespace DL_Game_Factory
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
