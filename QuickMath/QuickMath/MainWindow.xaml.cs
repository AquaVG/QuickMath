﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<CheckBox, char> relations = new Dictionary<CheckBox, char>();
        List<char> ActiveOps = new List<char>();

        System.Windows.Forms.Timer math_timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer memory_timer = new System.Windows.Forms.Timer();

        short Seconds = 0, Minutes = 3;
        public MainWindow()
        {
            InitializeComponent();
            relations.Add(CheckB_Add, '+');
            relations.Add(CheckB_Rem, '-');
            relations.Add(CheckB_Mult, '*');
            relations.Add(CheckB_Div, '/');

            math_timer.Tick += Math_timer_Tick;
            memory_timer.Tick += Memory_timer_Tick;
            math_timer.Interval = memory_timer.Interval = 1000;
        }

        private void MathTextB_KeyUp(object sender, KeyEventArgs e)
        {

        } 

        private void Memory_timer_Tick(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Math_timer_Tick(object sender, System.EventArgs e)
        {

            if (Seconds == 0 && Minutes >= 1)
            {
                Minutes--; Seconds = 60;
            }
            if (Minutes == 0 && Seconds == 0)
            {
                (sender as System.Windows.Forms.Timer).Enabled = false; return;
            }
            Seconds--;
            Math_Timer.Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";

        }

        private bool covered = false;
        private void Link_ChangeColor(object sender, MouseEventArgs e) => (sender as Label).Foreground = !(covered = !covered) ? Brushes.Black : Brushes.LightCoral;

        private void Link_Click(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            Label current = sender as Label;
            if (current.Tag.ToString() == "Math") MathGrid.Visibility = Visibility.Visible;
            else MemoryGrid.Visibility = Visibility.Visible;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ActiveOps.Clear();
            foreach (var pair in relations)
            {
                if (pair.Key.IsChecked == true)
                    ActiveOps.Add(pair.Value);
            }
            if (ActiveOps.Count < 2)
            {
                MessageBox.Show("You must select more than 2 operations.", "Error.");
                return;
            }
            SecondaryMathGrid.Visibility = Visibility.Hidden;
            MainMathGrid.Visibility = Visibility.Visible;
            Math_Timer.Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";
            math_timer.Enabled = true;
        }
    }
}
