using System;
using System.Collections.Generic;
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
        Dictionary<CheckBox, char> relationsCheckBOps = new Dictionary<CheckBox, char>();
        List<char> ActiveOps = new List<char>();

        System.Windows.Forms.Timer math_timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer memory_timer = new System.Windows.Forms.Timer();

        MathSkillWorker mathWorker;

        short Seconds = 0, Minutes = 3;
        public MainWindow()
        {
            InitializeComponent();

            MainGrid.Visibility = Visibility.Visible;
            MathGrid.Visibility = MainMathGrid.Visibility = Visibility.Hidden;

            relationsCheckBOps.Add(CheckB_Add, '+');
            relationsCheckBOps.Add(CheckB_Rem, '-');
            relationsCheckBOps.Add(CheckB_Mult, '*');
            relationsCheckBOps.Add(CheckB_Div, '/');

            math_timer.Tick += Math_timer_Tick;
            memory_timer.Tick += Memory_timer_Tick;
            math_timer.Interval = memory_timer.Interval = 1000;
        }

        private void MathTextB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(UAnswer_Math.Text, out int a))
                {
                    mathWorker.CheckAnswer(Math_Expression.Content.ToString(), Convert.ToInt32(UAnswer_Math.Text));
                    Math_Expression.Content = mathWorker.Expression;
                    UAnswer_Math.Text = "";
                }
            }
        }

        private void Memory_timer_Tick(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Math_timer_Tick(object sender, EventArgs e)
        {

            if (Seconds == 0 && Minutes >= 1)
            {
                Minutes--; Seconds = 60;
            }
            if (Minutes == 0 && Seconds == 0)
            {
                (sender as System.Windows.Forms.Timer).Enabled = false; 
                new InfoWindow(mathWorker.Right,mathWorker.Wrong).Show();
                return;
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
            foreach (var pair in relationsCheckBOps)
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
            mathWorker = new MathSkillWorker(ActiveOps);
            Math_Expression.Content = mathWorker.Expression;
        }
    }
}
