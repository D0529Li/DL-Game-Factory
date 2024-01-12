namespace DL_Game_Project
{
    [Serializable]
    public class Player
    {
        public string? Name { get; set; } = null;
        public int Score { get; set; } = 0;
        public SpeedOptions Speed { get; set; }

        #region constructors

        public Player() { Speed = SpeedOptions.Not_Selected; }

        public Player(SpeedOptions playerSpeed) { Speed = playerSpeed; }

        public Player(string playerName, int playerScore, SpeedOptions playerSpeed)
        {
            Name = playerName;
            Score = playerScore;
            Speed = playerSpeed;
        }

        public Player(Player player)
        {
            Name = player.Name;
            Score = player.Score;
            Speed = player.Speed;
        }

        #endregion
    }
}
