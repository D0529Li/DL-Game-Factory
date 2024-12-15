using System.Windows;
using System.Windows.Controls;

namespace Snake_Game
{
    /// <summary>
    /// Interaction logic for RecordsRemovalAuthentication.xaml
    /// </summary>
    public partial class RecordsRemovalAuthentication : Window
    {
        private string passwordEntered;
        private readonly string password = "lyc";
        public RecordsRemovalAuthentication()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            passwordEntered = (e.Source as TextBox).Text;
            if (CheckPassword())
            {
                DialogResult = true;
                Close();
            }
        }
        private bool CheckPassword()
        {
            if (passwordEntered == password) return true;
            return false;
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
