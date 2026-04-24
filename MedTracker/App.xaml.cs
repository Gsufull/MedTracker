using System;
using System.Windows;
using MedTracker.Views;

namespace MedTracker
{
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // При запуску показуємо вікно авторизації
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        // Перемикання між світлою і темною темою
        public static void ChangeTheme(bool isDark)
        {
            string source = isDark
                ? "Resources/Themes/Dark.xaml"
                : "Resources/Themes/Light.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri(source, UriKind.Relative)
            };
            // [0] — завжди тема
            Current.Resources.MergedDictionaries[0] = dict;
        }

        // Перемикання мови
        public static void ChangeLanguage(string langCode)
        {
            string source = langCode == "en"
                ? "Resources/Localization/Lang.en.xaml"
                : "Resources/Localization/Lang.ua.xaml";

            var dict = new ResourceDictionary
            {
                Source = new Uri(source, UriKind.Relative)
            };
            // [2] — завжди локалізація
            Current.Resources.MergedDictionaries[2] = dict;
        }
    }
}