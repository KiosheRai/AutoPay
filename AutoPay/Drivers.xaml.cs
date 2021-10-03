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
using System.Data;

namespace AutoPay
{
    /// <summary>
    /// Логика взаимодействия для Drivers.xaml
    /// </summary>
    public partial class Drivers : Page
    {
        public Drivers()
        {
            InitializeComponent();
            showInfo();
        }

        private void showInfo()
        {
            DataTable table = SQLbase.Select($"SELECT * FROM Driver");
            listDrivers.ItemsSource = table.DefaultView;
            
            CountDriversText.Content = $"Кол-во водителей: {table.Rows.Count}";
        }

        private void DeleteDriver_Click(object sender, RoutedEventArgs e)
        {
            int i = listDrivers.SelectedIndex;

            if (i == -1)
            {
                AlarmText.Visibility = Visibility.Visible;
                return;
            }

            AlarmText.Visibility = Visibility.Hidden;

            DataTable Table = SQLbase.Select($"SELECT * FROM Driver");

            if(Table.Rows[i][4] != "0")
            {
                SQLbase.Select($"delete Child where driver = {Table.Rows[i][0]}");
            }

            SQLbase.Select($"delete Driver where id = {Table.Rows[i][0]}");

            showInfo();
        }

        private void Button_Main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void Button_Mark(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void Button_DriverAdd(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddPage.xaml", UriKind.Relative));
        }

        private void ShowCurentDriver(object sender, RoutedEventArgs e)
        {
            int i = listDrivers.SelectedIndex;

            if (i == -1)
            {
                AlarmText.Visibility = Visibility.Visible;
                return;
            }

            AlarmText.Visibility = Visibility.Hidden;

            DataTable Table = SQLbase.Select($"SELECT * FROM Driver");


            NavigationService nav;
            nav = NavigationService.GetNavigationService(this);
            
            ShowDriverInfo nextPage = new ShowDriverInfo(Int32.Parse(Table.Rows[i][0].ToString()));
            nav.Navigate(nextPage);
        }
    }
}
