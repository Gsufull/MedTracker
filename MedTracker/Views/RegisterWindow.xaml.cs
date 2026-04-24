using System.Windows;
using MedTracker.Models;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password;
            string confirm = TxtConfirmPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirm))
            {
                ShowError((string)FindResource("Auth_ErrEmpty"));
                return;
            }

            if (password.Length < 4)
            {
                ShowError((string)FindResource("Auth_ErrShortPass"));
                return;
            }

            if (password != confirm)
            {
                ShowError((string)FindResource("Auth_ErrPassMatch"));
                return;
            }

            // Визначаємо роль
            UserRole role = CmbRole.SelectedIndex == 1 ? UserRole.Admin : UserRole.User;

            bool isRegistered = UserService.Register(username, password, role);

            if (isRegistered)
            {
                MessageBox.Show((string)FindResource("Auth_Success"), (string)FindResource("AppName"), MessageBoxButton.OK, MessageBoxImage.Information);
                var loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close();
            }
            else
            {
                ShowError((string)FindResource("Auth_ErrExists"));
            }
        }

        private void BtnGoLogin_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ShowError(string message)
        {
            TxtError.Text = message;
            TxtError.Visibility = Visibility.Visible;
        }
    }
}
