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
        TypeOfSearch tSearch;

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

        private void Button_Caclulate(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/PayMent.xaml", UriKind.Relative));
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

            foreach (Char x in countHours)
            {
                if (!char.IsDigit(x))
                {
                    formTime.ToolTip = "Требуется числовое значение!";
                    formTime.Foreground = Brushes.Red;
                    isGood = false;
                }
                else
                {
                    formTime.ToolTip = "";
                    formTime.Foreground = Brushes.Black;
                }
            }

            if (!isGood){ return; }


            DataTable tableDriver = SQLbase.Select($"SELECT * FROM Driver");

            DataTable tableMarks = SQLbase.Select($"select * from WorkDay where CONVERT(date,GETDATE()) like datee and driver = {tableDriver.Rows[i][0]}");

            if(tableMarks.Rows.Count > 0)
            {
                MessageBox.Show("Ошибка: водитель уже отмечен!");
                return;
            }

            if (day == TypeOfDays.working)
            {
                SQLbase.Insert($"INSERT INTO WorkDay(driver, datee, shiftt, daytype) values ({tableDriver.Rows[i][0]}, CONVERT(date,GETDATE()), {countHours}, 'Будний')");
            }
            else if(day == TypeOfDays.weekday)
            {
                SQLbase.Insert($"INSERT INTO WorkDay(driver, datee, shiftt, daytype) values ({tableDriver.Rows[i][0]}, CONVERT(date,GETDATE()), {countHours}, 'Выходной')");
            }
            else if(day == TypeOfDays.holiday)
            {
                SQLbase.Insert($"INSERT INTO WorkDay(driver, datee, shiftt, daytype) values ({tableDriver.Rows[i][0]}, CONVERT(date,GETDATE()), {countHours}, 'Праздник')");
            }

            MessageBox.Show("Водитель был отмечен!");
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

        private void showBonus_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "0123456789 ,".IndexOf(e.Text) < 0;
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
    }
}
