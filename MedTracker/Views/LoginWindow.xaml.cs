using System.Windows;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = TxtUsername.Text.Trim();
            string password = TxtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowError((string)FindResource("Auth_ErrEmpty"));
                return;
            }

            var user = UserService.Login(username, password);
            if (user != null)
            {
                SessionService.SetUser(user);
                
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                ShowError((string)FindResource("Auth_ErrWrong"));
            }
        }

        private void BtnGoRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();
        }

        private void ShowError(string message)
        {
            TxtError.Text = message;
            TxtError.Visibility = Visibility.Visible;
        }
    }
}
