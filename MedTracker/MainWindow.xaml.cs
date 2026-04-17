using System.Windows;
using MedTracker.Views; // Додаємо доступ до папки Views

namespace MedTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new HomePage());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InventoryPage());
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            // Передаємо параметр при переході на сторінку налаштувань
            MainFrame.Navigate(new SettingsPage("Адміністратор"));
        }

        // Тестову кнопку BtnTestSave_Click можна повністю видалити, вона нам більше не потрібна
    }
}