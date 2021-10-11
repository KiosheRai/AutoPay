using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Drivers(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Drivers.xaml", UriKind.Relative));
        }

        private void Button_Mark(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Mark.xaml", UriKind.Relative));
        }

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PayMent.xaml", UriKind.Relative));
        }

        private void OpenHelp(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo(Environment.CurrentDirectory + "\\Help.chm") { UseShellExecute = true });
        }
    }
}
