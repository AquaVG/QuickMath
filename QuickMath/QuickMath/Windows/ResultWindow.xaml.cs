using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace QuickMath.Windows
{
    /// <summary>
    /// Логика взаимодействия для ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(int Right, int Wrong, PracType type, ref User user)
        {
            InitializeComponent();
            Right_Label.Content = Right.ToString();
            Wrong_Label.Content = Wrong.ToString();
            Aps_Label.Content = Math.Round((Right + Wrong) / 180d, 2);
            Mark_Label.Content = (Right + Wrong != 0) ? (12 - (12 * Wrong) / (Right + Wrong)).ToString() : "1";
            if (type == PracType.Math)
                user.MathInfo.Score += Right - Wrong;
            else
                user.MemoryInfo.Score += Right - Wrong;
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\QuickMath\user.json", JsonConvert.SerializeObject(user, Formatting.Indented));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
