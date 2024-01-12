using System.IO;
using System.Windows;
using System.Xml.Serialization;

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



        //private GameOptions _gameOptions = new GameOptions();

        //private void ChoiceEnglish_Click(object sender, RoutedEventArgs e)
        //{
        //    _gameOptions.ChineseOrNo = false;
        //    saveGameOptions();
        //    DialogResult = true;
        //    Close();
        //}

        //private void ChoiceChinese_Click(object sender, RoutedEventArgs e)
        //{
        //    _gameOptions.ChineseOrNo = true;
        //    saveGameOptions();
        //    DialogResult = true;
        //    Close();
        //}
        //private void saveGameOptions()
        //{
        //    using (var stream = File.Open("GameOptions.xml", FileMode.Create))
        //    {
        //        var serializer = new XmlSerializer(typeof(GameOptions));
        //        serializer.Serialize(stream, _gameOptions);
        //    }
        //}
    }
}