using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DL_Game_Project
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class SnakeGame : Window, INotifyPropertyChanged
    {
        private const int GRID_SIZE = 13;

        public delegate void TimerDelegate();

        private Player player = new Player();
        private Candy candy = new Candy();
        private Snake? snake = null;

        private bool gameStarted = false;
        private SpeedOptions speed = SpeedOptions.Not_Selected;

        private SnakeGameViewModel _snakeGameViewModel;

        public SnakeGame()
        {
            InitializeComponent();
            DataContext = _snakeGameViewModel = new SnakeGameViewModel();
        }

        private void NewGameButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void RecordsButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Records.xml"))
            {
                Records? records;
                using (var stream = File.OpenRead("Records.xml"))
                {
                    var serializer = new XmlSerializer(typeof(Records));
                    records = serializer.Deserialize(stream) as Records;
                }
                StringBuilder s1 = new StringBuilder();
                StringBuilder s2 = new StringBuilder();
                StringBuilder s3 = new StringBuilder();
                if (records.RecordPlayer_Slow.Score != 0)
                    s1.Append($"Slow: {records.RecordPlayer_Slow.Name} - {records.RecordPlayer_Slow.Score}");
                if (records.RecordPlayer_Medium.Score != 0)
                    s2.Append($"Medium: {records.RecordPlayer_Medium.Name} - {records.RecordPlayer_Medium.Score}");
                if (records.RecordPlayer_Fast.Score != 0)
                    s3.Append($"Fast: {records.RecordPlayer_Fast.Name} - {records.RecordPlayer_Fast.Score}");
                MessageBox.Show(s1?.ToString() + "\n" + s2?.ToString() + "\n" + s3?.ToString());
            }
            else MessageBox.Show($"No records found. ");
        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        
        private void SpeedSlowButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedSlowButton.Background = new SolidColorBrush(Colors.YellowGreen);
            SpeedMediumButton.Background = new SolidColorBrush(Colors.Gray);
            SpeedFastButton.Background = new SolidColorBrush(Colors.Gray);
            speed = SpeedOptions.Slow;
        }
        private void SpeedMediumButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedSlowButton.Background = new SolidColorBrush(Colors.Gray);
            SpeedMediumButton.Background = new SolidColorBrush(Colors.YellowGreen);
            SpeedFastButton.Background = new SolidColorBrush(Colors.Gray);
            speed = SpeedOptions.Medium;
        }
        private void SpeedFastButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedSlowButton.Background = new SolidColorBrush(Colors.Gray);
            SpeedMediumButton.Background = new SolidColorBrush(Colors.Gray);
            SpeedFastButton.Background = new SolidColorBrush(Colors.YellowGreen);
            speed = SpeedOptions.Fast;
        }

        private void StartGame()
        {
            BuildGameGrid();
            _snakeGameViewModel.StartGame();
            //if (!gameStarted)
            //{
            //    _snake = new Snake(_player.Speed);
            //    Grid gameGrid = _snake.GameGrid;
            //    mainCanvas.Children.Add(gameGrid);
            //    gameGrid.SetValue(Canvas.LeftProperty, (double)600);
            //    gameGrid.SetValue(Canvas.TopProperty, (double)20);
            //    _snake.myTimer.Elapsed += CheckCandyEaten;
            //    gameStarted = true;
            //}
            //else _snake.ChangeSpeed(_player.Speed);
            //SetVisibility_GameGrid(Visibility.Visible);
            //SetVisibility_PauseAndExitButton(Visibility.Visible);
            //GenerateNewCandy();
            //_snake.TimerBegin();
            //CurrentScore = _snake.Length;
        }

        private void BuildGameGrid(int rows = GRID_SIZE, int cols = GRID_SIZE)
        {
            GameGrid.RowDefinitions.Clear();
            GameGrid.ColumnDefinitions.Clear();
            for (int i = 0; i < rows; i++)
            {
                var newRow = new RowDefinition
                {
                    Height = new GridLength(45), // auto?
                    Name = $"GameGridRow{i}",
                };
                
                GameGrid.RowDefinitions.Add(newRow);
            }
            for (int i = 0; i < cols; i++)
            {
                var newColumn = new ColumnDefinition
                {
                    Width = new GridLength(45), // auto?
                    Name = $"GameGridColumn{i}"
                };
                GameGrid.ColumnDefinitions.Add(newColumn);
            }

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < cols; j++)
                {
                    var newBorder = new Border
                    {
                        BorderBrush = new SolidColorBrush(Colors.Black),
                        BorderThickness = new Thickness(1),
                        Name = $"GameGridBorderR{i}C{j}"
                    };
                    Grid.SetRow(newBorder, i);
                    Grid.SetColumn(newBorder, j);
                    GameGrid.Children.Add(newBorder);
                }
            }

            GamePanel.Visibility = Visibility.Visible;
        }


        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            player.Name = PlayerNameTextBox.Text;
            player.Speed = speed;

            if (player.Name == null) MessageBox.Show("Please enter a valid name. ");
            else if (player.Speed == SpeedOptions.Not_Selected) MessageBox.Show("Please select a speed option.");
            else StartGame();
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
                records.ModifyRecords(player);
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
                    serializer.Serialize(stream, new Records(player));
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
        //private void PauseGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    PauseGameButton.Visibility = Visibility.Hidden;
        //    ResumeGameButton.Visibility = Visibility.Visible;
        //    _snake.TimerStop();
        //}
        //private void ResumeGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    PauseGameButton.Visibility = Visibility.Visible;
        //    ResumeGameButton.Visibility = Visibility.Hidden;
        //    _snake.TimerBegin();
        //}
        private void ExitGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
        private void AboutGameButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Place holder about game");
        }
        private void AboutMeButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Place holder about me");
        }

        #region INotifyPropertyChanged Implements

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
