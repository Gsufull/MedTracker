using System.Windows;

namespace MedTracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Обробники кнопок меню навігації
        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тут буде перехід на Головну сторінку.", "Навігація");
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тут буде перехід до Списку ліків.", "Навігація");
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Тут буде перехід до Налаштувань.", "Навігація");
        }

        // Обробник тестової кнопки збереження
        private void BtnTestSave_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtMedicineName.Text;
            string category = TxtCategory.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Будь ласка, введіть назву препарату!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Препарат: {name}\nКатегорія: {category}", "Успішно збережено", MessageBoxButton.OK, MessageBoxImage.Information);

            // Очищення полів після збереження
            TxtMedicineName.Clear();
            TxtCategory.Clear();
        }
    }
}