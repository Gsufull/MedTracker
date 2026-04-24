using System.Windows;
using System.Windows.Controls;
using MedTracker.Models;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class AdminPage : Page
    {
        public AdminPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DgUsers.ItemsSource = UserService.LoadUsers();
            ListLogs.ItemsSource = Logger.ReadLogs();
        }

        private void BtnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (DgUsers.SelectedItem is User selectedUser)
            {
                // Не дозволяємо видаляти самого себе
                if (selectedUser.Username == SessionService.CurrentUser?.Username)
                {
                    MessageBox.Show("Ви не можете видалити власний акаунт!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var result = MessageBox.Show((string)FindResource("Admin_ConfirmDelete"), 
                                             (string)FindResource("AppName"), 
                                             MessageBoxButton.YesNo, 
                                             MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    UserService.DeleteUser(selectedUser.Username);
                    Logger.Log($"Адміністратор видалив користувача {selectedUser.Username}");
                    LoadData();
                }
            }
        }

        private void BtnRefreshLogs_Click(object sender, RoutedEventArgs e)
        {
            ListLogs.ItemsSource = Logger.ReadLogs();
        }
    }
}
