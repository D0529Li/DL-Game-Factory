﻿namespace Snake_Game
{
    public class Candy
    {
        public Coordinate Coordinate { get; set; } = new Coordinate(-1, -1);
        public int GridSize { get; set; } = -1;
        public bool IsCandyValid { get; set; } = false;

        public Candy() { }

        public Candy(int gridSize)
        {
            GridSize = gridSize;
        }

        public Candy(int x, int y)
        {
            Coordinate.X = x;
            Coordinate.Y = y;
        }

        public void GenerateCandy(List<Coordinate> bodyPositions)
        {
            Random random = new Random();
            Coordinate.X = random.Next(0, GridSize);
            Coordinate.Y = random.Next(0, GridSize);
            while (bodyPositions.Contains(Coordinate))
            {
                Coordinate.X = random.Next(0, GridSize);
                Coordinate.Y = random.Next(0, GridSize);
            }
            IsCandyValid = true;
        }

        public void EatCandy()
        {
            IsCandyValid = false;
        }
    }
}
