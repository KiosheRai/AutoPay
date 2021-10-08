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
    /// Логика взаимодействия для PayMent.xaml
    /// </summary>
    public partial class PayMent : Page
    {
        TypeOfSearch tSearch;

        public PayMent()
        {
            InitializeComponent();
            showInfo();
        }

        private enum TypeOfSearch
        {
            name,
            id
        }

        private void showInfo()
        {
            DataTable table = SQLbase.Select($"SELECT * FROM Driver");
            listDrivers.ItemsSource = table.DefaultView;
        }

        private void showInfo(int id)
        {
            DataTable table = SQLbase.Select($"SELECT * FROM Driver where id = {id}");
            listDrivers.ItemsSource = table.DefaultView;
        }

        private void showInfo(string name)
        {
            DataTable table = SQLbase.Select($"SELECT * FROM Driver where FIO like '%{name}%'");
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

        private void RadioButton1_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton pressed = (RadioButton)sender;
            string s = pressed.Content.ToString();

            if (s == "По ФИО")
            {
                tSearch = TypeOfSearch.name;
                return;
            }
            if (s == "По ID")
            {
                tSearch = TypeOfSearch.id;
                return;
            }
        }

        private void Button_Find(object sender, RoutedEventArgs e)
        {
            string input = formSearch.Text;
            bool isGood = true;

            if (input.Trim() == "")
            {
                formSearch.ToolTip = "Заполните поле!";
                formSearch.Foreground = Brushes.Red;
                isGood = false;
            }
            else
            {
                formSearch.ToolTip = "";
                formSearch.Foreground = Brushes.Black; ;
            }

            if (tSearch == TypeOfSearch.name)
            {
                foreach (Char x in input)
                {
                    if (char.IsDigit(x))
                    {
                        formSearch.ToolTip = "Требуется символьное значение!";
                        formSearch.Foreground = Brushes.Red;
                        isGood = false;
                    }
                    else
                    {
                        formSearch.ToolTip = "";
                        formSearch.Foreground = Brushes.Black;

                    }
                }

                if (isGood)
                {
                    showInfo(input);
                }
            }
            else if (tSearch == TypeOfSearch.id)
            {
                foreach (Char x in input)
                {
                    if (!char.IsDigit(x))
                    {
                        formSearch.ToolTip = "Требуется числовое значение!";
                        formSearch.Foreground = Brushes.Red;
                        isGood = false;
                    }
                    else
                    {
                        formSearch.ToolTip = "";
                        formSearch.Foreground = Brushes.Black;

                    }
                }

                if (isGood)
                {
                    int g = Int32.Parse(input);
                    showInfo(g);
                }
            }
        }

        private void ButtonGoToCheque(object sender, RoutedEventArgs e)
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

            CulcPage nextPage = new CulcPage(Int32.Parse(Table.Rows[i][0].ToString()));
            nav.Navigate(nextPage);
        }
    }
}
