﻿using File_Organizer;
using System.ComponentModel;
using System.Windows.Input;

namespace DL_Game_Factory
{
    public class SnakeGameViewModel : INotifyPropertyChanged
    {
        private static int maxIndex = 29;
        public Player player { get; set; }
        public int Score { get; set; }
        public bool MainControlPanelVisibility { get; set; }
        public bool NewGamePanelVisibility { get; set; }

        public delegate void ArrowKeyPressedHandler(Direction direction);
        public event ArrowKeyPressedHandler? ArrowKeyPressed;

        public SpeedOptions Speed { get; set; }

        #region Commands

        public static RoutedCommand GoLeftCommand = new RoutedCommand("Go Left", typeof(SnakeGameViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Left) }));

        public static RoutedCommand GoRightCommand = new RoutedCommand("Go Right", typeof(SnakeGameViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Right) }));

        public static RoutedCommand GoUpCommand = new RoutedCommand("Go Up", typeof(SnakeGameViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Up) }));

        public static RoutedCommand GoDownCommand = new RoutedCommand("Go Down", typeof(SnakeGameViewModel),
            new InputGestureCollection(new List<InputGesture> { new KeyGesture(Key.Down) }));

        public ICommand LeftArrowKeyCommand { get; set; }
        public ICommand RightArrowKeyCommand { get; set; }
        public ICommand UpArrowKeyCommand { get; set; }
        public ICommand DownArrowKeyCommand { get; set; }

        public ICommand NewGameCommand { get; set; }
        public ICommand StartGameCommand { get; set; }

        #endregion Commands

        public SnakeGameViewModel()
        {
            NewGameCommand = new DelegateCommand<object>(NewGame);
            StartGameCommand = new DelegateCommand<object>(StartGame);
            LeftArrowKeyCommand = new DelegateCommand<object>(OnPressLeftArrowKey);
            RightArrowKeyCommand = new DelegateCommand<object>(OnPressRightArrowKey);
            UpArrowKeyCommand = new DelegateCommand<object>(OnPressUpArrowKey);
            DownArrowKeyCommand = new DelegateCommand<object>(OnPressDownArrowKey);

            MainControlPanelVisibility = true;
            NewGamePanelVisibility = false;
            OnPropertyChanged(nameof(MainControlPanelVisibility));
            OnPropertyChanged(nameof(NewGamePanelVisibility));

            player = new Player();
            Score = 0;
        }

        public void NewGame(object param)
        {
            NewGamePanelVisibility = true;
            OnPropertyChanged(nameof(NewGamePanelVisibility));
        }

        //public void PauseGame()
        //{

        //}

        //public void StopGame()
        //{

        //}

        public void StartGame(object param)
        {
            // TBD: CHECK
            var values = (object[])param;
            player.Name = values[0] as string;

            switch (values[1] as string)
            {
                case "Slow": Speed = SpeedOptions.Slow; break;
                case "Medium": Speed = SpeedOptions.Medium; break;
                case "Fast": Speed = SpeedOptions.Fast; break;
                default: Speed = SpeedOptions.Not_Selected; break;
            }
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