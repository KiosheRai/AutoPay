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
    /// Логика взаимодействия для Mark.xaml
    /// </summary>
    public partial class Mark : Page
    {
        TypeOfDays day;

        public Mark()
        {
            InitializeComponent();
            showInfo();
        }

        private enum TypeOfDays
        {
            working,
            weekday,
            holiday
        }

        private void showInfo()
        {
            DataTable table = SQLbase.Select($"SELECT * FROM Driver");
            listDrivers.ItemsSource = table.DefaultView;
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
            throw new Exception();
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string s = pressed.Content.ToString();

            if (s == "Будний")
            {
                day = TypeOfDays.working;
                return;
            }
            if (s == "Выходной")
            {
                day = TypeOfDays.weekday;
                return;
            }
            if (s == "Праздник")
            {
                day = TypeOfDays.holiday;
                return;
            }
        }

        private void ButtonAddMark(object sender, RoutedEventArgs e)
        {
            string countHours = formTime.Text.Trim();
            int i = listDrivers.SelectedIndex;
            bool isGood = true;

            if (i == -1)
            {
                AlarmText.Visibility = Visibility.Visible;
                isGood = false;
            }
            else
            {
                AlarmText.Visibility = Visibility.Hidden;
            }

            if (countHours == "")
            {
                isGood = false;

                formTime.ToolTip = "Заполните поле!";
                formTime.Foreground = Brushes.Red;
            }
            else
            {
                formTime.ToolTip = "";
                formTime.Foreground = Brushes.Black;
            }

            if (!isGood){ return; }


            DataTable tableDriver = SQLbase.Select($"SELECT * FROM Driver");

            DataTable tableMarks = SQLbase.Select($"SELECT * FROM WorkDay where daytype like ''");
        }
    }
}
