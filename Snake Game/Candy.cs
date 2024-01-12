namespace DL_Game_Factory
{
    public class Candy
    {
        public int X { get; set; } = -1;
        public int Y { get; set; } = -1;
        public bool IsCandyValid { get; set; } = false;

        public Candy() { }

        public Candy(int gridSize)
        {
            GenerateCandy(gridSize);
            IsCandyValid = true;
        }

        public void GenerateCandy(int maxIndex)
        {
            Random random = new Random();
            X = random.Next(0, maxIndex);
            Y = random.Next(0, maxIndex);
            IsCandyValid = true;
        }

        public void EatCandy()
        {
            IsCandyValid = false;
        }
    }
}
