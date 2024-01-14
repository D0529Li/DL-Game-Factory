using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace DL_Game_Factory
{
    /// <summary>
    /// Interaction logic for StartGame.xaml
    /// </summary>
    public partial class SnakeGame : Window, INotifyPropertyChanged
    {

        public delegate void TimerDelegate();

        private Player player = new Player();
        private Snake snake = new Snake();

        // private bool gameStarted = false;
        private SpeedOptions speed = SpeedOptions.Not_Selected;

        private SnakeGameViewModel snakeGameVM;

        public SnakeGame()
        {
            InitializeComponent();
            DataContext = snakeGameVM = new SnakeGameViewModel();
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

        private void SnakeMovedHandler(Coordinate oldPos, Coordinate newPos)
        {
            RenderSnakeOnGameGrid(oldPos, newPos);
        }

        private void BuildGameGrid(int rows = SnakeConstants.DEFAULT_GRID_SIZE, int cols = SnakeConstants.DEFAULT_GRID_SIZE)
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
                GameGrid.RegisterName(newRow.Name, newRow);
            }
            for (int i = 0; i < cols; i++)
            {
                var newColumn = new ColumnDefinition
                {
                    Width = new GridLength(45), // auto?
                    Name = $"GameGridColumn{i}"
                };
                GameGrid.ColumnDefinitions.Add(newColumn);
                GameGrid.RegisterName(newColumn.Name, newColumn);
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
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
                    GameGrid.RegisterName(newBorder.Name, newBorder);
                }
            }

            GamePanel.Visibility = Visibility.Visible;
        }

        private void RenderSnakeOnGameGrid()
        {
            foreach (var position in snake.GetBodyPositions())
            {
                Dispatcher.Invoke(() =>
                {
                    if (GameGrid.FindName($"GameGridBorderR{position.X}C{position.Y}") is Border border)
                        border.Background = new SolidColorBrush(Colors.Black);
                });
            }
        }

        private void RenderSnakeOnGameGrid(Coordinate oldPos, Coordinate newPos)
        {
            Dispatcher.Invoke(() =>
            {
                // TBD: This is currently getting hit if snake dies. 

                if (GameGrid.FindName($"GameGridBorderR{oldPos.X}C{oldPos.Y}") is Border oldBorder)
                    oldBorder.Background = new SolidColorBrush(Colors.Transparent);

                if (GameGrid.FindName($"GameGridBorderR{newPos.X}C{newPos.Y}") is Border newBorder)
                    newBorder.Background = new SolidColorBrush(Colors.Black);
            });
        }

        private void RenderCandyOnGameGrid(Candy? oldCandy = null)
        {
            Dispatcher.Invoke(() =>
            {
                if (oldCandy != null && oldCandy.Coordinate.IsValid())
                {
                    if (GameGrid.FindName($"GameGridBorderR{oldCandy.Coordinate.X}C{oldCandy.Coordinate.Y}") is Border oldCandyBorder)
                        oldCandyBorder.Background = new SolidColorBrush(Colors.Black);
                }
                
                if (GameGrid.FindName($"GameGridBorderR{snake.Candy.Coordinate.X}C{snake.Candy.Coordinate.Y}") is Border newCandyBorder)
                    newCandyBorder.Background = new SolidColorBrush(Colors.YellowGreen);
            });
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            player.Name = PlayerNameTextBox.Text;
            player.Speed = speed;

            if (player.Name == null)
            {
                MessageBox.Show("Please enter a valid name. "); 
                return;
            }
            if (player.Speed == SpeedOptions.Not_Selected)
            {
                MessageBox.Show("Please select a speed option.");
                return;
            }
            player.Speed = speed;
            player.Name = PlayerNameTextBox.Text;

            snake.SnakeMoved += SnakeMovedHandler;
            snake.SnakeDies += SnakeDiesHandler;
            snake.SnakeEatsCandy += SnakeEatsCandyHandler;
            snakeGameVM.ArrowKeyPressed += snake.ChangeDirection;

            snake.Initialize(speed);
            BuildGameGrid();
            snakeGameVM.StartGame();
            RenderSnakeOnGameGrid();
            RenderCandyOnGameGrid();
            snake.StartGame();
        }

        private void SnakeDiesHandler(SnakeDiesExceptions ex)
        {
            // SaveRecord();
        }

        private void SnakeEatsCandyHandler(Candy oldCandy)
        {
            RenderCandyOnGameGrid(oldCandy);
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

        private void PauseGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PauseGameButton.Visibility = Visibility.Collapsed;
            ResumeGameButton.Visibility = Visibility.Visible;
            snake.PauseGame();
        }

        private void ResumeGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PauseGameButton.Visibility = Visibility.Visible;
            ResumeGameButton.Visibility = Visibility.Collapsed;
            snake.ResumeGame();
        }

        private void StopGameButton_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
