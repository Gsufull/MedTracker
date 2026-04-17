using System.Windows.Controls;

namespace MedTracker.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
           
        }

        private bool isDark = false;
        private void BtnTheme_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            isDark = !isDark;
            App.ChangeTheme(isDark);
            (sender as System.Windows.Controls.Button).Content = isDark ? "Увімкнути світлу тему" : "Увімкнути темну тему";
        }
    }
}