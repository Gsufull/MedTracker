using System.Windows;
using System.Windows.Controls;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            InitializeComponent();
            LoadUserInfo();
        }

        private void LoadUserInfo()
        {
            if (SessionService.CurrentUser != null)
            {
                TxtCurrentUser.Text = SessionService.CurrentUser.Username;
                TxtRole.Text = SessionService.CurrentUser.Role == Models.UserRole.Admin 
                    ? (string)FindResource("Auth_RoleAdmin") 
                    : (string)FindResource("Auth_RoleUser");
            }
        }

        private void BtnThemeLight_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme(false);
        }

        private void BtnThemeDark_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeTheme(true);
        }

        private void BtnLangUA_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeLanguage("ua");
            LoadUserInfo(); // Оновлюємо текст ролі після зміни мови
        }

        private void BtnLangEN_Click(object sender, RoutedEventArgs e)
        {
            App.ChangeLanguage("en");
            LoadUserInfo(); // Оновлюємо текст ролі після зміни мови
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionService.Logout();
            
            var window = Window.GetWindow(this);
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            
            window?.Close();
        }
    }
}