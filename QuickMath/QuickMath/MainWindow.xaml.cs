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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickMath
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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

        }
    }
}
