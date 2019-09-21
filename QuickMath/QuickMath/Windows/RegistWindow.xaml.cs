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
        public RegistWindow()
        {
            InitializeComponent();
            App.Language = new CultureInfo("en-US");
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

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.DefaultLanguage = App.Language;
            User user = new User(Name_TextBox.Text, new PracticeTypeInfo(Math_IntegerUpDown.Value ?? 1, 0), new PracticeTypeInfo(Memory_IntegerUpDown.Value ?? 1, 0));
            File.WriteAllText("user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
            Properties.Settings.Default.Save();
            new MainWindow().Show();
            Close();
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox thisTB = sender as TextBox;
            Regist_Button.IsEnabled = thisTB.Text != "" && new Regex(@"^([A-Z]|[А-Я]|Ї|І|Є).+$").IsMatch(thisTB.Text);
            
        }
    }
}
