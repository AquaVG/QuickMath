using Newtonsoft.Json;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuickMath.SkillWorkers;
using QuickMath.Windows;
using System.Diagnostics;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)+@"\QuickMath\user.json";
        System.Windows.Forms.Timer math_timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer memory_timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer hide_timer = new System.Windows.Forms.Timer();

        MathSkillWorker mathWorker;
        MemorySkillWorker memoryWorker;

        User user;
 
        readonly short[ ] timerdata = new short[2];
        short Seconds, Minutes;
        public MainWindow()
        {
            InitializeComponent();
            Minutes = 3;
            Seconds = 0;
#if DEBUG || DW
            userFilePath = "user.json";
            Debug.WriteLine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\QuickMath\user.json");
            Minutes = 0;
            Seconds = 10;
#endif
            timerdata[0] = Seconds;
            timerdata[1] = Minutes;

            StartVisibility();
            UpdateUserInfo();
            DataContext = this;
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

            Memory_Expression.Visibility = Visibility.Visible;
        }
        private void UpdateUserInfo()
        {
            user = JsonConvert.DeserializeObject<User>(File.ReadAllText(userFilePath));

            PbMathLvl.Value = user.MathInfo.Score;
            PbMemoryLvl.Value = user.MemoryInfo.Score;

 
            mathWorker = new MathSkillWorker(user.MathInfo.Level);
            memoryWorker = new MemorySkillWorker(user.MemoryInfo.Level);

            string opsList = "";
            foreach (char item in mathWorker.EnableActions)
            {
                opsList += item + " ";
            }
            MathOps_Label.Content = opsList.Remove(opsList.Length - 1);
            MathLevel_Label.Content = user.MathInfo.Level;

            MemoryLevel_Label.Content = user.MemoryInfo.Level;
            MemoryLenght_Label.Content = memoryWorker.Lenght;
            MemoryTime_Label.Content = memoryWorker.SecondsToHide;
        }
 
 
        private void BackButton_Click(object sender, MouseButtonEventArgs e)
        {
            StartVisibility();
            math_timer.Enabled = memory_timer.Enabled = false;
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
 
                    hide_timer.Stop();
                    hide_timer.Start();
 
                    Memory_Expression.Visibility = Visibility.Visible;
                    UAnswer_Memory.Text = "";
                }
            }
        }
        private void MathTextB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                string uAns = UAnswer_Math.Text.Replace('.', ',');
                if (double.TryParse(uAns, out double a))
                {
                    mathWorker.CheckAnswer(Math_Expression.Content.ToString(), Convert.ToDouble(uAns));
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
                    new ResultWindow(mathWorker.Right, mathWorker.Wrong, PracType.Math, ref user).ShowDialog();
                else
                    new ResultWindow(memoryWorker.Right, memoryWorker.Wrong, PracType.Memory, ref user).ShowDialog();
 
                StartVisibility();
                UpdateUserInfo();
                return;
            }  
            Seconds--;
            (timer.Tag.ToString() == "math" ? Math_Timer : Memory_Timer).Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";
        }
        private void Hide_MemoryTimer_Tick(object sender, EventArgs e)
        {
            Memory_Expression.Visibility = Visibility.Hidden;
 
            hide_timer.Enabled = false;
 
            hide_timer.Stop();
 
        }

        private void MemoryButton_Click(object sender, RoutedEventArgs e)
        {
            user = JsonConvert.DeserializeObject<User>(File.ReadAllText(userFilePath));
            SecondaryMemoryGrid.Visibility = Visibility.Hidden;
            MainMemoryGrid.Visibility = Visibility.Visible;
            memory_timer.Enabled = true;
             Seconds = timerdata[0];
            Minutes = timerdata[1];
            Memory_Timer.Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";
 
            memoryWorker = new MemorySkillWorker(user.MemoryInfo.Level);
            Memory_Expression.Content = memoryWorker.Expression;
            hide_timer.Interval = memoryWorker.SecondsToHide * 1000;
            hide_timer.Enabled = true;
            UAnswer_Memory.Text = "";
            UAnswer_Memory.Focus();
        }
        private void MathButton_Click(object sender, RoutedEventArgs e)
        {
            user = JsonConvert.DeserializeObject<User>(File.ReadAllText(userFilePath));
            SecondaryMathGrid.Visibility = Visibility.Hidden;
            MainMathGrid.Visibility = Visibility.Visible;
            math_timer.Enabled = true;
            Seconds = timerdata[0];
            Minutes = timerdata[1];
            Math_Timer.Content = Seconds >= 10 ? $"0{Minutes}:{Seconds}" : $"0{Minutes}:0{Seconds}";
            Math_Expression.Content = mathWorker.Expression;
            UAnswer_Math.Text = "";
            UAnswer_Math.Focus();
        }
        private bool covered = false;
        private void Link_ChangeColor(object sender, MouseEventArgs e) => (sender as Label).Foreground = !(covered = !covered)
                                                                                          ? Brushes.Black
 
                                                                                          : (SolidColorBrush)(new BrushConverter().ConvertFrom("#0ee1ff"));
        private void SettingsIcon_Click (object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                MainGrid.Visibility = Visibility.Hidden;
            }
        } 
        private void Link_Click(object sender, MouseButtonEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            Label current = sender as Label;
            if (current.Tag.ToString() == "Math") MathGrid.Visibility = Visibility.Visible;
            else MemoryGrid.Visibility = Visibility.Visible;
        }
    }
}
