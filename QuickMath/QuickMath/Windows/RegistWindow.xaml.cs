using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
using AquaDir;
using System.Text.RegularExpressions;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для RegistWindow.xaml
    /// </summary>
    public partial class RegistWindow : Window
    {
        CultureInfo eng = new CultureInfo("en-US");
        CultureInfo rus = new CultureInfo("ru-RU");
        CultureInfo ukr = new CultureInfo("uk-UA");
        public RegistWindow()
        {
            InitializeComponent();
            App.Language = new CultureInfo("en-US");
            RegistBtn.IsEnabled = false;
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\QuickMath");
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    App.Language = eng;
                    break;
                case 1:
                    App.Language = rus;
                    break;
                case 2:
                    App.Language = ukr;
                    break;
                default:
                    MessageBox.Show("Couldn\'t find localization file.", "Error");
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
#if RELEASE
            User user = new User(Name_TextBox.Text, new PracticeTypeInfo(Math_IntegerUpDown.Value ?? 1), new PracticeTypeInfo(Memory_IntegerUpDown.Value ?? 1));
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\QuickMath\user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
            Properties.Settings.Default.Save();
            new MainWindow().Show();
            Close();
#else
            User user = new User(Name_TextBox.Text, new PracticeTypeInfo(Math_IntegerUpDown.Value ?? 1), new PracticeTypeInfo(Memory_IntegerUpDown.Value ?? 1));
            File.WriteAllText("user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
            Properties.Settings.Default.Save();
            new MainWindow().Show();
            Close();
#endif
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegistBtn.IsEnabled = new Regex(@"^([A-Z]|[А-Я]|Ї|І|Є).+$").IsMatch((sender as TextBox).Text);            
        }
    }
}
