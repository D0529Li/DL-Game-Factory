namespace DL_Game_Factory
{
    public class Candy
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public int GridSize { get; set; } = -1;
        public bool IsCandyValid { get; set; } = false;

        public Candy() { }

        public Candy(int gridSize)
        {
            GridSize = gridSize;
        }

        public void GenerateCandy()
        {
            Random random = new Random();
            X = random.Next(0, GridSize);
            Y = random.Next(0, GridSize);
            IsCandyValid = true;
        }

        public void EatCandy()
        {
            IsCandyValid = false;
        }
    }
}
