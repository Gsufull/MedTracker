using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using MedTracker.Services;
using MedTracker.Views;

namespace MedTracker
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer _reminderTimer;
        private HashSet<string> _triggeredReminders = new HashSet<string>();

        public MainWindow()
        {
            InitializeComponent();
            
            // Якщо є залогінений користувач
            if (SessionService.CurrentUser != null)
            {
                TxtWelcome.Text = $"{(string)FindResource("Nav_WelcomeUser")} {SessionService.CurrentUser.Username}";

                if (SessionService.IsAdmin)
                {
                    BtnAdmin.Visibility = Visibility.Visible;
                }
            }

            MainFrame.Navigate(new HomePage());
            ResetButtonStyles(BtnHome);

            SetupReminderTimer();
        }

        private void SetupReminderTimer()
        {
            _reminderTimer = new DispatcherTimer();
            _reminderTimer.Interval = TimeSpan.FromSeconds(30);
            _reminderTimer.Tick += ReminderTimer_Tick;
            _reminderTimer.Start();
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            var reminders = ReminderService.LoadReminders();
            string currentTime = DateTime.Now.ToString("HH:mm");

            foreach (var reminder in reminders)
            {
                if (reminder.Time == currentTime)
                {
                    string uniqueKey = $"{reminder.MedicineName}_{currentTime}_{DateTime.Now.ToShortDateString()}";
                    if (!_triggeredReminders.Contains(uniqueKey))
                    {
                        _triggeredReminders.Add(uniqueKey);
                        MessageBox.Show($"Нагадування: час прийняти {reminder.MedicineName} ({reminder.Dosage})!", 
                            "MedTracker Нагадування", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new HomePage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnInventory_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new InventoryPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnReminders_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new RemindersPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnStatistics_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new StatisticsPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnFirstAid_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new FirstAidPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new SettingsPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnAdmin_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new AdminPage());
            ResetButtonStyles(sender as System.Windows.Controls.Button);
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            SessionService.Logout();
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private void ResetButtonStyles(System.Windows.Controls.Button activeButton)
        {
            var defaultStyle = (Style)FindResource("SidebarBtnStyle");
            var activeStyle = (Style)FindResource("SidebarBtnActiveStyle");

            BtnHome.Style = defaultStyle;
            BtnInventory.Style = defaultStyle;
            BtnReminders.Style = defaultStyle;
            BtnStatistics.Style = defaultStyle;
            BtnFirstAid.Style = defaultStyle;
            BtnSettings.Style = defaultStyle;
            BtnAdmin.Style = defaultStyle;

            if (activeButton != null)
            {
                activeButton.Style = activeStyle;
            }
        }
    }
}