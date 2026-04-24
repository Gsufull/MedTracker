using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class StatisticsPage : Page
    {
        public StatisticsPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var medicines = MedicineService.LoadMedicines();

            TxtTotalKinds.Text = medicines.Count.ToString();
            TxtTotalQuantity.Text = medicines.Sum(m => m.Quantity).ToString();
            TxtExpiredCount.Text = medicines.Count(m => m.IsExpired).ToString();
            TxtCategoriesCount.Text = medicines.Select(m => m.Category).Where(c => !string.IsNullOrWhiteSpace(c)).Distinct().Count().ToString();
        }
    }
}
