using System.Windows;

namespace Snake_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnSnakeButtonClick(object sender, RoutedEventArgs e)
        {
            var snakeWindow = new Snake_Game.SnakeGame();
            snakeWindow.Show();
        }
    }
}