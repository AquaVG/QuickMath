using System;
using System.Windows;
using System.Windows.Controls;

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
            App.Language = new System.Globalization.CultureInfo("en-US");
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            App.Language = new System.Globalization.CultureInfo((sender as ComboBox).SelectedIndex == 0 ? "en-US" : "ru-RU");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.IsRegisted = true;
            Properties.Settings.Default.Name = Name_TextBox.Text;
            Properties.Settings.Default.MathL = Math_IntegerUpDown.Value ?? 1;
            Properties.Settings.Default.MemoryL = Memory_IntegerUpDown.Value ?? 1;
            new MainWindow().Show();
            Close();
        }
    }
}
