namespace DL_Game_Factory.Snake_Game
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
