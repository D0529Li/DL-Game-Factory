namespace DL_Game_Project
{
    [Serializable]
    public class Player
    {
        public string name { get; set; }
        public int score { get; set; }
        public SpeedOptions speed { get; set; }
        public Player() { score = 0; }
        public Player(SpeedOptions playerSpeed) : this() { speed = playerSpeed; }
        public Player(string playerName, int playerScore, SpeedOptions playerSpeed)
        {
            name = playerName;
            score = playerScore;
            speed = playerSpeed;
        }
        public Player(Player player)
        {
            name = player.name;
            score = player.score;
            speed = player.speed;
        }
    }
}
