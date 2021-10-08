using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для CulcPage.xaml
    /// </summary>
    public partial class CulcPage : Page
    {
        private int id;

        public CulcPage()
        {
            InitializeComponent();
            MessageBox.Show("Отладка!");
        }

        public CulcPage(int _id)
        {
            this.id = _id;
            InitializeComponent();

            showName();
        }

        private void showName()
        {
            DataTable table = SQLbase.Select($"select FIO from Driver where id = {id}");
            NameOfDriver.Text = table.Rows[0][0].ToString();
        }

        private void Button_Main(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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

        private void GoWord(object sender, RoutedEventArgs e)
        {
            throw new Exception();
        }

        private void CuclButton(object sender, RoutedEventArgs e)
        {
            DateTime StartDate;
            DateTime EndDate;

            if (DateStart.SelectedDate == null)
            {
                AlarmMessage.Content = "Введите начальную дату!";
                return;
            }
            
            if (DateEnd.SelectedDate == null)
            {
                AlarmMessage.Content = "Введите конечную дату!";
                return;
            }

            StartDate = DateStart.SelectedDate.Value;
            EndDate = DateEnd.SelectedDate.Value;

            if(StartDate >= EndDate)
            {
                AlarmMessage.Content = "Введите верную дату!";
                return;
            }

            AlarmMessage.Content = "";


            
        }
    }
}
