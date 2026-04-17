using System;
using System.Windows;

namespace MedTracker
{
    public partial class App : Application
    {
        public static void ChangeTheme(bool isDark)
        {
            var dict = new ResourceDictionary();
            dict.Source = isDark ? new Uri("Resources/Themes/Dark.xaml", UriKind.Relative)
                                 : new Uri("Resources/Themes/Light.xaml", UriKind.Relative);

            // Замінюємо перший словник (яким є наша тема)
            Current.Resources.MergedDictionaries[0] = dict;
        }
    }
}