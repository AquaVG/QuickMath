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
        System.Windows.Forms.Timer hide_timer = new System.Windows.Forms.Timer();

        MathSkillWorker mathWorker;
        MemorySkillWorker memoryWorker;

        short Seconds = 0, Minutes = 3;
        public MainWindow()
        {
            InitializeComponent();

            StartVisibility();

            relationsCheckBOps.Add(CheckB_Add, '+');
            relationsCheckBOps.Add(CheckB_Rem, '-');
            relationsCheckBOps.Add(CheckB_Mult, '*');
            relationsCheckBOps.Add(CheckB_Div, '/');

            UAnswer_Memory.Text = "";

            math_timer.Tick += Timer_Tick;
            memory_timer.Tick += Timer_Tick;
            hide_timer.Tick += Hide_MemoryTimer_Tick;

            math_timer.Tag = "math";
            memory_timer.Tag = "memory";

            math_timer.Interval = memory_timer.Interval = 1000;
        }

        private void StartVisibility()
        {
            MainGrid.Visibility = SecondaryMathGrid.Visibility = SecondaryMemoryGrid.Visibility = Visibility.Visible;
            MathGrid.Visibility = MainMathGrid.Visibility = MainMemoryGrid.Visibility = MemoryGrid.Visibility = Visibility.Hidden;
        }

        private void MemoryTextB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (int.TryParse(UAnswer_Memory.Text, out int a) && UAnswer_Memory.Text.Length == Memory_Expression.Content.ToString().Length)
                {
                    memoryWorker.CheckAnswer((string)Memory_Expression.Content, UAnswer_Memory.Text);
                    Memory_Expression.Content = memoryWorker.Expression;
                    hide_timer.Enabled = true;
                    Memory_Expression.Visibility = Visibility.Visible;
                    UAnswer_Memory.Text = "";
                }
            }
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
        private void Timer_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = sender as System.Windows.Forms.Timer;
            if (Seconds == 0 && Minutes >= 1)
            {
                Minutes--;
                Seconds = 60;
            }
            if (Minutes == 0 && Seconds == 0)
            {
                timer.Enabled = false;
                if (timer.Tag.ToString() == "math")
                    new InfoWindow(mathWorker.Right, mathWorker.Wrong).Show();
                else
                    new InfoWindow(memoryWorker.Right, memoryWorker.Wrong).Show();
                StartVisibility();
                return;
            }
            Seconds--;
            (timer.Tag.ToString() == "math" ? Math_Timer : Memory_Timer).Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";
        }
        private void Hide_MemoryTimer_Tick(object sender, EventArgs e)
        {
            Memory_Expression.Visibility = Visibility.Hidden;
            hide_timer.Enabled = false;
        }

        private void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            SecondaryMemoryGrid.Visibility = Visibility.Hidden;
            MainMemoryGrid.Visibility = Visibility.Visible;
            memory_timer.Enabled = true;
            Memory_Timer.Content = $"0{Minutes}:00";
            memoryWorker = new MemorySkillWorker(LenghtOfNum_IntegerUD.Value ?? 4);
            Memory_Expression.Content = memoryWorker.Expression;
            hide_timer.Interval = (SecondsToHide_IntegerUD.Value ?? 1) * 1000;
            hide_timer.Enabled = true;
        }
        private void MathButton_Click(object sender, RoutedEventArgs e)
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
            math_timer.Enabled = true;
            Math_Timer.Content = $"0{Minutes}:00";
            mathWorker = new MathSkillWorker(ActiveOps);
            Math_Expression.Content = mathWorker.Expression;
        }
        private bool covered = false;
        private void Link_ChangeColor(object sender, MouseEventArgs e) => (sender as Label).Foreground = !(covered = !covered) 
                                                                                          ? Brushes.Black 
                                                                                          : (SolidColorBrush)(new BrushConverter().ConvertFrom("#0ee1ff"));
        private void Link_Click(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            Label current = sender as Label;
            if (current.Tag.ToString() == "Math") MathGrid.Visibility = Visibility.Visible;
            else MemoryGrid.Visibility = Visibility.Visible;
        }


    }
}
