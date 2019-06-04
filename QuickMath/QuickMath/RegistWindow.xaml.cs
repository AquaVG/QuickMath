﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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

        }
    }
}
