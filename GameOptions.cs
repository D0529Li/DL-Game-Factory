namespace DL_Game_Project
{
    [Serializable]
    public class GameOptions
    {
        private bool _chineseOrNo;
        public bool ChineseOrNo
        {
            get => _chineseOrNo;
            set
            {
                _chineseOrNo = value;
            }
        }
    }
}
