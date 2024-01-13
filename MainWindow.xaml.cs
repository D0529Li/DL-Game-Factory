using System.Windows;

namespace DL_Game_Factory
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
            var snakeWindow = new SnakeGame();
            snakeWindow.Show();
        }
    }
}