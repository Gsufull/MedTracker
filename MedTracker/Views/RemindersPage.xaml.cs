using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MedTracker.Models;
using MedTracker.Services;

namespace MedTracker.Views
{
    public partial class RemindersPage : Page
    {
        private ObservableCollection<Reminder> _reminders;

        public RemindersPage()
        {
            InitializeComponent();
            var loadedReminders = ReminderService.LoadReminders();
            _reminders = new ObservableCollection<Reminder>(loadedReminders);
            
            DgReminders.ItemsSource = _reminders;
            LoadMedicines();
        }

        private void LoadMedicines()
        {
            var medicines = MedicineService.LoadMedicines();
            CmbMedicines.ItemsSource = medicines;
            if (medicines.Any())
            {
                CmbMedicines.SelectedIndex = 0;
            }
        }

        private void BtnAddReminder_Click(object sender, RoutedEventArgs e)
        {
            if (CmbMedicines.SelectedItem == null || string.IsNullOrWhiteSpace(TxtTime.Text) || string.IsNullOrWhiteSpace(TxtDosage.Text))
            {
                MessageBox.Show((string)FindResource("Reminders_ErrFill"), (string)FindResource("Reminders_ErrTitle"), MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _reminders.Add(new Reminder
            {
                MedicineName = CmbMedicines.SelectedValue.ToString(),
                Time = TxtTime.Text,
                Dosage = TxtDosage.Text
            });

            ReminderService.SaveReminders(_reminders);

            TxtTime.Clear();
            TxtDosage.Clear();
        }

        private void BtnDeleteReminder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reminder = button?.DataContext as Reminder;
            if (reminder != null)
            {
                _reminders.Remove(reminder);
                ReminderService.SaveReminders(_reminders);
            }
        }
    }
}
