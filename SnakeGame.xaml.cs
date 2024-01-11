using System.ComponentModel;
using System.IO;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace DL_Game_Project
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class SnakeGame : Window, INotifyPropertyChanged
    {
        public delegate void TimerDelegate();
        public event EventHandler<CandyEventArgs> CandyEaten;

        private GameOptions _gameOptions;
        private SnakeModel _snake;
        private Player _player = new Player();
        private int _candyX;
        private int _candyY;
        private bool gameStarted = false;
        private int _currentScore;
        public int CurrentScore
        {
            get
            {
                return _currentScore;
            }
            set
            {
                _currentScore = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentScore)));
            }
        }

        #region TextStrings
        public string StringTitle { get; set; }
        public string StringSubtitle { get; set; }
        public string StringOption1 { get; set; }
        public string StringOption2 { get; set; }
        public string StringOption3 { get; set; }
        public string StringQuerySpeed { get; set; }
        public string StringQueryName { get; set; }
        public string StringSnakeDies { get; set; }
        public string StringSelfCollision { get; set; }
        public string StringCollisionWithWall { get; set; }
        public string StringScoreDisplay { get; set; }
        public string StringConfirm { get; set; }
        public string StringConfirmWithNoName { get; set; }
        public string StringConfirmWithNoSpeed { get; set; }
        public string StringRecordDisplay1 { get; set; }
        public string StringRecordDisplay2 { get; set; }
        public string StringRecordNoRecord { get; set; }
        public string StringSpeedSlow { get; set; }
        public string StringSpeedMedium { get; set; }
        public string StringSpeedFast { get; set; }
        public string StringCurrentScore { get; set; }
        public string StringAboutGame { get; set; }
        public string StringAboutMe { get; set; }
        public string ContentAboutGame { get; set; }
        public string ContentAboutMe { get; set; }
        public Thickness Margin_Title { get; set; }
        public Thickness Margin_Subtitle { get; set; }

        #endregion TextStrings

        public SnakeGame()
        {
            InitializeComponent();
            var welcomeDialog = new MainWindow();
            welcomeDialog.ShowDialog();

            CandyEaten += OnCandyEaten;
            SetStringsAndOptions();
            SetVisibility_Options(Visibility.Visible);
            SetVisibility_QueryNameAndSpeed(Visibility.Hidden);
            _snake = null;

        }
        private void SetStringsAndOptions()
        {
            string path = Environment.CurrentDirectory + "\\Images";
            if (_gameOptions == null)
            {
                if (File.Exists("GameOptions.xml"))
                {
                    using (var stream = File.OpenRead("GameOptions.xml"))
                    {
                        var serializer = new XmlSerializer(typeof(GameOptions));
                        _gameOptions = serializer.Deserialize(stream) as GameOptions;
                    }
                }
            }
            if (_gameOptions.ChineseOrNo)
            {
                LanguagePackages.CopyTo(LanguageChoices.Chinese, this);

            }
            else
            {
                LanguagePackages.CopyTo(LanguageChoices.English, this);
            }
            DataContext = this;
        }
        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            QueryForNameAndSpeed();
        }
        private void RecordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Records.xml"))
            {
                Records records;
                using (var stream = File.OpenRead("Records.xml"))
                {
                    var serializer = new XmlSerializer(typeof(Records));
                    records = serializer.Deserialize(stream) as Records;
                }
                StringBuilder s1 = new StringBuilder();
                StringBuilder s2 = new StringBuilder();
                StringBuilder s3 = new StringBuilder();
                if (records.RecordPlayer_Slow.score != 0)
                    s1.Append($"{StringSpeedSlow}: {records.RecordPlayer_Slow.name} - {records.RecordPlayer_Slow.score}");
                if (records.RecordPlayer_Medium.score != 0)
                    s2.Append($"{StringSpeedMedium}: {records.RecordPlayer_Medium.name} - {records.RecordPlayer_Medium.score}");
                if (records.RecordPlayer_Fast.score != 0)
                    s3.Append($"{StringSpeedFast}: {records.RecordPlayer_Fast.name} - {records.RecordPlayer_Fast.score}");
                MessageBox.Show(s1?.ToString() + "\n" + s2?.ToString() + "\n" + s3?.ToString());
            }
            else MessageBox.Show($"{StringRecordNoRecord}");
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void SpeedButton_Click(object sender, RoutedEventArgs e)
        {
            var button = e.Source as Button;
            if (_player.speed != SpeedOptions.Not_Selected)
            {
                if (_player.speed == SpeedOptions.Slow) speedButtonSlow.Background = new SolidColorBrush(Colors.Gray);
                else if (_player.speed == SpeedOptions.Medium) speedButtonMedium.Background = new SolidColorBrush(Colors.Gray);
                else speedButtonFast.Background = new SolidColorBrush(Colors.Gray);
            }
            button.Background = new SolidColorBrush(Colors.Green);
            if (button == speedButtonSlow) _player.speed = SpeedOptions.Slow;
            else if (button == speedButtonMedium) _player.speed = SpeedOptions.Medium;
            else _player.speed = SpeedOptions.Fast;
        }

        private void SetVisibility_Options(Visibility visibilityOption)
        {
            NewGameButton.Visibility =
            RecordsButton.Visibility =
            ExitButton.Visibility = visibilityOption;
        }

        private void SetVisibility_QueryNameAndSpeed(Visibility visibilityOption)
        {
            // Speed query buttons
            querySpeedTextBlock.Visibility =
            speedButtonSlow.Visibility =
            speedButtonMedium.Visibility =
            speedButtonFast.Visibility = visibilityOption;
            
            // Name query buttons
            queryNameBlock.Visibility =
            nameBox.Visibility =
            confirmButton.Visibility = visibilityOption;
        }

        private void SetVisibility_GameGrid(Visibility visibilityOption)
        {
            _snake.GameGrid.Visibility = visibilityOption;
        }

        private void SetVisibility_PauseAndExitButton(Visibility visibilityOption)
        {
            PauseGameButton.Visibility = visibilityOption;
            ExitGameButton.Visibility = visibilityOption;
        }

        private void StartNewGame()
        {
            SetVisibility_Options(Visibility.Hidden);
            SetVisibility_QueryNameAndSpeed(Visibility.Hidden);
            if (!gameStarted)
            {
                _snake = new SnakeModel(_player.speed);
                Grid gameGrid = _snake.GameGrid;
                mainCanvas.Children.Add(gameGrid);
                gameGrid.SetValue(Canvas.LeftProperty, (double)600);
                gameGrid.SetValue(Canvas.TopProperty, (double)20);
                _snake.myTimer.Elapsed += CheckCandyEaten;
                gameStarted = true;
            }
            else _snake.ChangeSpeed(_player.speed);
            SetVisibility_GameGrid(Visibility.Visible);
            SetVisibility_PauseAndExitButton(Visibility.Visible);
            GenerateNewCandy();
            _snake.TimerBegin();
            CurrentScore = _snake.Length;
        }
        private void RemoveOldCandy()
        {
            _snake.RemoveCandy(_candyX, _candyY);
        }
        private void GenerateNewCandy()
        {
            Random sourceGen = new Random(Guid.NewGuid().GetHashCode());
            int x = sourceGen.Next(0, SnakeModel.maxIndex);
            int y = sourceGen.Next(0, SnakeModel.maxIndex);
            while (!_snake.CheckSnakePos(x, y))
            {
                x = sourceGen.Next(0, SnakeModel.maxIndex);
                y = sourceGen.Next(0, SnakeModel.maxIndex);
            }
            _snake.SetCandy(x, y);
            _candyX = x;
            _candyY = y;
        }
        private void OnCandyEaten(object sender, CandyEventArgs e)
        {
            RemoveOldCandy();
            GenerateNewCandy();
            _snake.Length++;
            CurrentScore++;
            _snake.MoveHead();
        }
        private void CommandCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == SnakeModel.GoLeftCommand || e.Command == SnakeModel.GoRightCommand
                || e.Command == SnakeModel.GoUpCommand || e.Command == SnakeModel.GoDownCommand)
            {
                if (_snake != null) e.CanExecute = true;
                else e.CanExecute = false;
            }
            else e.CanExecute = false;
            e.Handled = true;
        }
        private void CommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == SnakeModel.GoLeftCommand)
            {
                _snake.HeadDirection = Directions.left;
            }
            else if (e.Command == SnakeModel.GoRightCommand)
            {
                _snake.HeadDirection = Directions.right;
            }
            else if (e.Command == SnakeModel.GoUpCommand)
            {
                _snake.HeadDirection = Directions.up;
            }
            else if (e.Command == SnakeModel.GoDownCommand)
            {
                _snake.HeadDirection = Directions.down;
            }
        }
        public void CheckCandyEaten(object sender, ElapsedEventArgs e)
        {
            if (_snake.HeadX == _candyX && _snake.HeadY == _candyY)
                CandyEaten(this, new CandyEventArgs(_snake));
            else if (!_snake.Move())
                _snake.GameGrid.Dispatcher.BeginInvoke(new TimerDelegate(SnakeHasDied));
        }
        private void SnakeHasDied()
        {
            _player.score = _snake.Length;
            SaveRecord();
            SetVisibility_GameGrid(Visibility.Hidden);
            SetVisibility_PauseAndExitButton(Visibility.Hidden);
            SetVisibility_Options(Visibility.Visible);
            _snake.ResetSnake();
        }
        private void QueryForNameAndSpeed()
        {
            SetVisibility_QueryNameAndSpeed(Visibility.Visible);
        }
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (_player.name == null) MessageBox.Show($"{StringConfirmWithNoName}");
            else if (_player.speed == SpeedOptions.Not_Selected) MessageBox.Show($"{StringConfirmWithNoSpeed}");
            else StartNewGame();
        }
        private void CollectName(object sender, RoutedEventArgs e)
        {
            TextBox nameBox = mainCanvas.FindName("nameBox") as TextBox;
            _player.name = nameBox.Text;
        }
        private void SaveRecord()
        {
            if (File.Exists("Records.xml"))
            {
                Records records;
                using (var stream = File.OpenRead("Records.xml"))
                {
                    var serializer = new XmlSerializer(typeof(Records));
                    records = serializer.Deserialize(stream) as Records;
                }
                records.ModifyRecords(_player);
                using (var stream = File.Open("Records.xml", FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(Records));
                    serializer.Serialize(stream, records);
                }
            }
            else
            {
                using (var stream = File.Open("Records.xml", FileMode.Create))
                {
                    var serializer = new XmlSerializer(typeof(Records));
                    serializer.Serialize(stream, new Records(_player));
                }
            }
        }
        private void RemoveRecordsRequest(object sender, RoutedEventArgs e)
        {
            var authentication = new RecordsRemovalAuthentication();
            authentication.ShowDialog();
            if (authentication.DialogResult == true) RemoveAllRecords();
        }
        private void RemoveAllRecords()
        {
            if (File.Exists("Records.xml"))
            {
                File.Delete("Records.xml");
                MessageBox.Show("All records have been removed.");
            }
            else
            {
                MessageBox.Show("No record found.");
            }
        }
        private void PauseGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PauseGameButton.Visibility = Visibility.Hidden;
            ResumeGameButton.Visibility = Visibility.Visible;
            _snake.TimerStop();
        }
        private void ResumeGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PauseGameButton.Visibility = Visibility.Visible;
            ResumeGameButton.Visibility = Visibility.Hidden;
            _snake.TimerBegin();
        }
        private void ExitGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void AboutGameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ContentAboutGame);
        }
        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(ContentAboutMe);
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
