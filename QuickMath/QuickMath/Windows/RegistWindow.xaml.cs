using System;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using System.IO;
using Newtonsoft.Json;
<<<<<<< HEAD
using AquaDir;
=======
>>>>>>> Progressbar added, timer bug fixed
using System.Text.RegularExpressions;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для RegistWindow.xaml
    /// </summary>
    public partial class RegistWindow : Window
    {
        public RegistWindow()
        {
            InitializeComponent();
            App.Language = new CultureInfo("en-US");
<<<<<<< HEAD
            Regist_Button.IsEnabled = false;
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            CultureInfo current;
            int thisComboBIndex = (sender as ComboBox).SelectedIndex;
            if (thisComboBIndex.IsOneOf(1, 2))
                current = thisComboBIndex == 1 ? new CultureInfo("ru-RU") : new CultureInfo("uk-UA");
            else
                current = new CultureInfo("en-US");
            App.Language = current;

=======
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            App.Language = new CultureInfo((sender as ComboBox).SelectedIndex == 0 ? "en-US" : "ru-RU");
>>>>>>> Progressbar added, timer bug fixed
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            Properties.Settings.Default.DefaultLanguage = App.Language;
=======
>>>>>>> Progressbar added, timer bug fixed
            User user = new User(Name_TextBox.Text, new PracticeTypeInfo(Math_IntegerUpDown.Value ?? 1, 0), new PracticeTypeInfo(Memory_IntegerUpDown.Value ?? 1, 0));
            File.WriteAllText("user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
            Properties.Settings.Default.Save();
            new MainWindow().Show();
            Close();
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
<<<<<<< HEAD
            TextBox thisTB = sender as TextBox;
            Regist_Button.IsEnabled = thisTB.Text != "" && new Regex(@"^([A-Z]|[А-Я]|Ї|І|Є).+$").IsMatch(thisTB.Text);
=======
            RegistBtn.IsEnabled = new Regex(@"^([A-Z]|[А-Я]|Ї|І|Є).+$").IsMatch((sender as TextBox).Text);
>>>>>>> Progressbar added, timer bug fixed
            
        }
    }
}
