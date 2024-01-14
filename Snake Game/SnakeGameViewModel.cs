using File_Organizer;
using System.ComponentModel;
using System.Windows.Input;

namespace DL_Game_Factory
{
    public class SnakeGameViewModel : INotifyPropertyChanged
    {
        private int _score { get; set; } = 0;
        public Player Player { get; set; } = new Player();
        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool MainControlPanelVisibility { get; set; }
        public bool NewGamePanelVisibility { get; set; }

        public delegate void ArrowKeyPressedHandler(Direction direction);
        public event ArrowKeyPressedHandler? ArrowKeyPressed;

        #region Commands

        public ICommand LeftArrowKeyCommand { get; set; }
        public ICommand RightArrowKeyCommand { get; set; }
        public ICommand UpArrowKeyCommand { get; set; }
        public ICommand DownArrowKeyCommand { get; set; }
        public ICommand MakeNewGameCommand { get; set; }
        public ICommand StartGameCommand { get; set; }

        #endregion Commands

        public SnakeGameViewModel()
        {
            MakeNewGameCommand = new DelegateCommand<object>(MakeNewGame);
            StartGameCommand = new DelegateCommand<object>(StartGame);
            LeftArrowKeyCommand = new DelegateCommand<object>(OnPressLeftArrowKey);
            RightArrowKeyCommand = new DelegateCommand<object>(OnPressRightArrowKey);
            UpArrowKeyCommand = new DelegateCommand<object>(OnPressUpArrowKey);
            DownArrowKeyCommand = new DelegateCommand<object>(OnPressDownArrowKey);

            MainControlPanelVisibility = true;
            NewGamePanelVisibility = false;
            OnPropertyChanged(nameof(MainControlPanelVisibility));
            OnPropertyChanged(nameof(NewGamePanelVisibility));

            Score = 0;
        }

        public void MakeNewGame(object param)
        {
            NewGamePanelVisibility = true;
            OnPropertyChanged(nameof(NewGamePanelVisibility));
        }

        public void StartGame(object param)
        {
            // TBD: CHECK
            Player.Name = param.ToString();
            Score = 4;
        }

        public void StartGame()
        {
            MainControlPanelVisibility = false;
            NewGamePanelVisibility = false;
            OnPropertyChanged(nameof(MainControlPanelVisibility));
            OnPropertyChanged(nameof(NewGamePanelVisibility));
        }

        public void OnPressLeftArrowKey(object param)
        {
            ArrowKeyPressed?.Invoke(Direction.Left);
        }

        public void OnPressRightArrowKey(object param)
        {
            ArrowKeyPressed?.Invoke(Direction.Right);
        }

        public void OnPressUpArrowKey(object param)
        {
            ArrowKeyPressed?.Invoke(Direction.Up);
        }

        public void OnPressDownArrowKey(object param)
        {
            ArrowKeyPressed?.Invoke(Direction.Down);
        }

        #region INotifyPropertyChanged Implements

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion
    }
}
