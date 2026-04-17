using System.Windows.Controls;

namespace MedTracker.Views
{
    public partial class SettingsPage : Page
    {
        public SettingsPage(string userName)
        {
            InitializeComponent();
            TextBlock txt = new TextBlock
            {
                Text = $"Налаштування для користувача: {userName}",
                FontSize = 24,
                HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                VerticalAlignment = System.Windows.VerticalAlignment.Center
            };
            this.Content = txt;
        }
    }
}