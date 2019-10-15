using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
        {
            InitializeComponent();
            LanguageChanged += App_LanguageChanged;

            Languages.Clear();
            Languages.Add(new CultureInfo("en-US")); //Нейтральная культура для этого проекта
            Languages.Add(new CultureInfo("ru-RU"));

            Language = QuickMath.Properties.Settings.Default.DefaultLanguage;
        }

        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                try
                {
                    switch (value.Name)
                    {
                        case "en-US":
                            dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                            break;
                        default:
                            dict.Source = new Uri($"Resources/lang.{value.Name}.xaml", UriKind.Relative);
                            break;
                    }
                }
                catch (Exception) { } //Have no idea for what, but without this, it doesnt work

                var oldDict = (from d in Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                               select d).First();
                if (oldDict != null)
                {
                    int ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(object sender, EventArgs e)
        {
            QuickMath.Properties.Settings.Default.DefaultLanguage = Language;
            QuickMath.Properties.Settings.Default.Save();

        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (File.Exists("user.json"))
                new MainWindow().Show();
            else
                new RegistWindow().Show();
        }
    }
}
