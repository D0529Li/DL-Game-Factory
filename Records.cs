namespace DL_Game_Project
{
    [Serializable]
    public class Records
    {
        private Player recordPlayer_Slow = new Player(SpeedOptions.Slow);
        private Player recordPlayer_Medium = new Player(SpeedOptions.Medium);
        private Player recordPlayer_Fast = new Player(SpeedOptions.Fast);
        public Player RecordPlayer_Slow { get { return recordPlayer_Slow; } set { recordPlayer_Slow = value; } }
        public Player RecordPlayer_Medium { get { return recordPlayer_Medium; } set { recordPlayer_Medium = value; } }
        public Player RecordPlayer_Fast { get { return recordPlayer_Fast; } set { recordPlayer_Fast = value; } }
        private Records() { }
        public Records(Player player) : this()
        {
            ModifyRecords(player);
        }
        public void ModifyRecords(Player player)
        {
            if (player.speed == SpeedOptions.Slow && player.score > recordPlayer_Slow.score)
            {
                recordPlayer_Slow.name = player.name;
                recordPlayer_Slow.score = player.score;
            }
            else if (player.speed == SpeedOptions.Medium && player.score > recordPlayer_Medium.score)
            {
                recordPlayer_Medium.name = player.name;
                recordPlayer_Medium.score = player.score;
            }
            else if (player.speed == SpeedOptions.Fast && player.score > recordPlayer_Fast.score)
            {
                recordPlayer_Fast.name = player.name;
                recordPlayer_Fast.score = player.score;
            }
        }
    }
}
