using System;
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
    /// Логика взаимодействия для InfoWindow.xaml
    /// </summary>
    public partial class InfoWindow : Window
    {
        public InfoWindow(int Right, int Wrong)
        {
            InitializeComponent();
            Right_Label.Content = Right.ToString();
            Wrong_Label.Content = Wrong.ToString();
            Aps_Label.Content = Math.Round((Right + Wrong) / 180d,2);
            Mark_Label.Content = (Right + Wrong != 0) ? (12 - (12 * Wrong) / (Right + Wrong)).ToString() : "1";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
