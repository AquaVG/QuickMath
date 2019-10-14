﻿using System;
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
            RegistBtn.IsEnabled = false;
        }
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            App.Language = new CultureInfo((sender as ComboBox).SelectedIndex == 0 ? "en-US" : "ru-RU");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User(Name_TextBox.Text, new PracticeTypeInfo(Math_IntegerUpDown.Value ?? 1), new PracticeTypeInfo(Memory_IntegerUpDown.Value ?? 1));
            File.WriteAllText("user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
            Properties.Settings.Default.Save();
            new MainWindow().Show();
            Close();
        }

        private void Name_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            RegistBtn.IsEnabled = new Regex(@"^([A-Z]|[А-Я]|Ї|І|Є).+$").IsMatch((sender as TextBox).Text);            
        }
    }
}