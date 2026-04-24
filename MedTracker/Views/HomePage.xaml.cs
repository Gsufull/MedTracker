using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            var meds = MedicineService.LoadMedicines();

            int total = meds.Count;
            int expired = meds.Count(m => m.IsExpired);
            int lowStock = meds.Count(m => m.Quantity > 0 && m.Quantity <= 5);

            TxtTotal.Text = total.ToString();
            TxtExpired.Text = expired.ToString();
            TxtLowStock.Text = lowStock.ToString();

            if (expired > 0)
            {
                BrdExpiredAlert.Visibility = Visibility.Visible;
            }
        }

        private void BtnGoInventory_Click(object sender, RoutedEventArgs e)
        {
            // Пошук головного вікна і перехід до списку ліків
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new InventoryPage());
            }
        }
    }
}
